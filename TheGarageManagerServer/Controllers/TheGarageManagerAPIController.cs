using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using TheGarageManagerServer.DTO;
using TheGarageManagerServer.GarageAPIReaderService;
using TheGarageManagerServer.Models;

[Route("api")]
[ApiController]
public class TheGarageManagerAPIController : ControllerBase
{
    //a variable to hold a reference to the db context!
    private TheGarageManagerDbContext context;
    //a variable that hold a reference to web hosting interface (that provide information like the folder on which the server runs etc...)
    private IWebHostEnvironment webHostEnvironment;
    //Use dependency injection to get the db context and web host into the constructor
    public TheGarageManagerAPIController(TheGarageManagerDbContext context, IWebHostEnvironment env)
    {
        this.context = context;
        this.webHostEnvironment = env;
    }

    [HttpGet]
    [Route("TestServer")]
    public ActionResult<string> TestServer()
    {
        return Ok("Server Responded Successfully");
    }


    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginInfo loginDto)
    {
        try
        {
            HttpContext.Session.Clear(); //Logout any previous login attempt

            //Get model user class from DB with matching email. 
            User modelsUser = context.GetUser(loginDto.Email);

            //Check if user exist for this email and if password match, if not return Access Denied (Error 403) 
            if (modelsUser == null || modelsUser.UserPassword != loginDto.Password)
            {
                return Unauthorized();
            }

            //Login suceed! now mark login in session memory!
            HttpContext.Session.SetString("loggedInUser", modelsUser.Email);

            UserDTO dtoUser = new UserDTO(modelsUser);
            //dtoUser.ProfileImagePath = GetProfileImageVirtualPath(dtoUser.Id);
            return Ok(dtoUser);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserDTO userDto, [FromQuery] string rashamHavarot)
    {
        try
        {
            HttpContext.Session.Clear(); //Logout any previous login attempt

            //First check if the garage is registered in minstery of transportation
            GarageService gService = new GarageService();
            Garage? g = await gService.ImportGarageFromApiAsync(rashamHavarot);

            if (g == null)
            {
                return NotFound();
            }

            //check if the garage is in the db. if it does not exist, add it to the db
            Garage? inDb = context.Garages.Where(gg => gg.RashamHavarot == rashamHavarot).FirstOrDefault();
            if (inDb == null)
            {
                context.Garages.Add(g);
                context.SaveChanges();
                userDto.UserGarageID = g.GarageId;
            }
            else
            {
                userDto.UserGarageID = inDb.GarageId;
            }

            //Create model user class
            User modelsUser = userDto.GetModels();

            context.Users.Add(modelsUser);
            context.SaveChanges();

            //User was added!
            UserDTO dtoUser = new UserDTO(modelsUser);
            //dtoUser.ProfileImagePath = GetProfileImageVirtualPath(dtoUser.Id);
            return Ok(dtoUser);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }



    [HttpGet("GetUserStatuses")]
    public IActionResult GetUserStatuses()
    {
        try
        {
            List<UserStatus> statuses = context.UserStatuses.ToList();

            List<UserStatusDTO> result = new List<UserStatusDTO>();
            foreach (UserStatus status in statuses)
            {
                result.Add(new UserStatusDTO(status));
            }
            
            
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }


    [HttpPost("getCarRepairs")]
    public IActionResult GetAllCarRepairs([FromBody] TheGarageManagerServer.DTO.LicensePlateDTO licensePlateDto)
    {
        try
        {
            string l = licensePlateDto.LicensePlate;
            ObservableCollection<CarRepair> vehicleRepairs = context.GetRepairs(l);
            List<string> GaragesNames = new List<string>();
            ObservableCollection<CarRepairDTO> vehicleRepairsDto = new ObservableCollection<CarRepairDTO>();
            foreach (CarRepair v in vehicleRepairs)
            {
                CarRepairDTO vDto = new CarRepairDTO(v);
                vDto.GarageName = context.GetGarageName(v.GarageId);
                vehicleRepairsDto.Add(vDto);
            }
            return Ok(vehicleRepairsDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


   

    [HttpGet("getAllGarageParts")]
    public IActionResult GetAllGarageParts()
    {
        try
        {
            // שליפת כל החלקים מטבלת GarageParts
            var garageParts = context.GarageParts.ToList();

            // יצירת רשימה של DTOים לכל החלקים
            List<GaragePartsDTO> garagePartsDto = new List<GaragePartsDTO>();

            foreach (var part in garageParts)
            {
                // המרת כל פריט לאובייקט DTO
                GaragePartsDTO partDto = new GaragePartsDTO
                {
                    GarageID = part.GarageId,
                    PartName = part.PartName,
                    PartNumber = part.PartNumber,
                    Cost = part.Cost,
                    ImageURL = part.ImageUrl
                };
                garagePartsDto.Add(partDto);
            }

            // החזרת המידע כ-OK עם רשימת החלקים
            return Ok(garagePartsDto);
        }
        catch (Exception ex)
        {
            // טיפול בשגיאות
            return BadRequest(ex.Message);
        }
    }


    [HttpPost("updateUser")]
    public IActionResult UpdateUser([FromBody] UserDTO userDto)
    {
        try
        {
            HttpContext.Session.Clear(); //Logout any previous login attempt

            //Create model user class
            User modelsUser = userDto.GetModels();

            context.Users.Update(modelsUser);
            context.SaveChanges();

            //User was added!
            UserDTO dtoUser = new UserDTO(modelsUser);
            //dtoUser.ProfileImagePath = GetProfileImageVirtualPath(dtoUser.Id);
            return Ok(dtoUser);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }




    [HttpPost("addAppointment")]
    public IActionResult AddAppointment([FromBody] AppointmentDTO appointmentDto)
    {
        try
        {
            // בדיקה אם ה- LicensePlate ו- GarageID קיימים בטבלאות המתאימות
            var garageExists = context.Garages.Any(g => g.GarageId == appointmentDto.GarageID);
            if (!garageExists)
            {
                return BadRequest("The specified Garage does not exist.");
            }

            var vehicleExists = context.Vehicles.Any(v => v.LicensePlate == appointmentDto.LicensePlate);
            if (!vehicleExists)
            {
                return BadRequest("The specified Vehicle does not exist.");
            }

            // יצירת אובייקט Appointment ממודל ה-DTO
            Appointment modelsAppointment = appointmentDto.GetModels();

            // סטטוס ברירת מחדל הוא "Pending" (0)
            modelsAppointment.AppointmentStatusId = 0; // 0 הוא סטטוס Pending לפי הטבלה שלך

            // הוספת הפגישה למסד הנתונים
            context.Appointments.Add(modelsAppointment);
            context.SaveChanges(); // אם הכל תקין, נשמור את השינויים במסד הנתונים

            // החזרת המידע המפוקס מחדש ב-DTO
            AppointmentDTO dtoAppointment = new AppointmentDTO(modelsAppointment);
            return Ok(dtoAppointment);
        }
        catch (Exception ex)
        {
            // במקרה של שגיאה
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }



    [HttpGet("getAppointments")]
    public IActionResult GetAppointments()
    {
        try
        {

            //check logged in
            string? email = HttpContext.Session.GetString("loggedInUser");
            if (email == null)
            {
                return Unauthorized();
            }
            // שליפת המשתמש מהמסד נתונים
            User? user = context.GetUser(email);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            int garageId = user.UserGarageId;
            // שליפת כל הבקשות מהמסד נתונים (Appointments)
            var appointments = context.Appointments.Where(a=>a.GarageId == garageId) // סינון לפי ה-GarageId של המשתמש
                                      .Include(a => a.Garage) // אם אתה רוצה להחזיר גם את פרטי ה-Garage
                                      .Include(a => a.LicensePlateNavigation) // אם אתה רוצה להחזיר גם את פרטי הרכב*****************
                                      .Include(a => a.AppointmentStatus) // אם אתה רוצה להחזיר את פרטי הסטטוס
                                      .ToList();

            // המרת הבקשות ל-DTO (כדי לא לחשוף ישירות את המודלים של הנתונים)
            var appointmentDtos = appointments.Select(a => new AppointmentDTO(a)).ToList();

            // החזרת התשובה למשתמש
            return Ok(appointmentDtos);
        }
        catch (Exception ex)
        {
            // במקרה של שגיאה, מחזירים את הודעת השגיאה
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }

    [HttpGet("getCars")]
    public IActionResult GetCars()
    {
        try
        {

            //check logged in
            string? email = HttpContext.Session.GetString("loggedInUser");
            if (email == null)
            {
                return Unauthorized();
            }
            // שליפת המשתמש מהמסד נתונים
            User? user = context.GetUser(email);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            int garageId = user.UserGarageId;
            // שליפת כל הרכבים מהמסד נתונים ()
            var garage = context.Garages.Where(g => g.GarageId == garageId)
                                        .Include(g => g.Appointments).ThenInclude(a => a.LicensePlateNavigation).ThenInclude(v=>v.CarRepairs).FirstOrDefault();
                                        

            List<VehicleDTO> vehicleDtos = new List<VehicleDTO>();

            foreach(var appointment in garage.Appointments)
            {
                if (appointment.LicensePlateNavigation != null)
                {
                    VehicleDTO vehicleDto = new VehicleDTO(appointment.LicensePlateNavigation);
                    vehicleDtos.Add(vehicleDto);
                }
            }
            return Ok(vehicleDtos);
        }
        catch (Exception ex)
        {
            // במקרה של שגיאה, מחזירים את הודעת השגיאה
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }




    [HttpGet("getAppointmentStatuses")]
    public IActionResult GetAppointmentStatuses()
    {
        try
        {
            // שליפת כל הסטטוסים מהטבלה AppointmentStatus
            var statuses = context.AppointmentStatuses.ToList();

            // המרת הסטטוסים ל-DTO (כדי להחזיר אותם בצורה נוחה)
            var statusDtos = statuses.Select(s => new AppointmentStatusDTO(s)).ToList();

            // החזרת הסטטוסים בתשובה
            return Ok(statusDtos);
        }
        catch (Exception ex)
        {
            // במקרה של שגיאה, מחזירים את הודעת השגיאה
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }




    [HttpPost("updateAppointmentStatus")]
    public IActionResult UpdateAppointmentStatus([FromBody] AppointmentDTO updateDto)
    {
        try
        {
            // שליפת הפגישה לפי AppointmentID
            var appointment = context.Appointments.FirstOrDefault(a => a.AppointmentId == updateDto.AppointmentID);

            if (appointment == null)
            {
                return NotFound("Appointment not found.");
            }

            // בדיקה אם הסטטוס שנשלח קיים
            var statusExists = context.AppointmentStatuses.Any(s => s.StatusId == updateDto.AppointmentStatusId);

            if (!statusExists)
            {
                return BadRequest("Invalid status ID.");
            }

            // עדכון הסטטוס לפגישה
            appointment.AppointmentStatusId = updateDto.AppointmentStatusId;

            // שמירת השינויים במסד הנתונים
            context.SaveChanges();

            // החזרת הפגישה עם הסטטוס החדש
            var updatedAppointmentDto = new AppointmentDTO(appointment);
            return Ok(updatedAppointmentDto);
        }
        catch (Exception ex)
        {
            // במקרה של שגיאה
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }



    [HttpPost("addCarRepair")]
    public IActionResult AddCarRepair([FromBody] CarRepairDTO carRepairDto)
    {
        try
        {
            // בדיקה אם ה- LicensePlate ו- GarageID קיימים בטבלאות המתאימות
            var garageExists = context.Garages.Any(g => g.GarageId == carRepairDto.GarageID);
            if (!garageExists)
            {
                return BadRequest("The specified Garage does not exist.");
            }

            var vehicleExists = context.Vehicles.Any(v => v.LicensePlate == carRepairDto.LicensePlate);
            if (!vehicleExists)
            {
                return BadRequest("The specified Vehicle does not exist.");
            }

            // יצירת אובייקט CarRepair ממודל ה-DTO
            CarRepair modelsCarRepair = carRepairDto.GetModels();

            // הוספת תיקון הרכב למסד הנתונים
            context.CarRepairs.Add(modelsCarRepair);
            context.SaveChanges(); // אם הכל תקין, נשמור את השינויים במסד הנתונים

            // החזרת המידע המפוקס מחדש ב-DTO
            CarRepairDTO dtoCarRepair = new CarRepairDTO(modelsCarRepair);
            return Ok(dtoCarRepair);
        }
        catch (Exception ex)
        {
            // במקרה של שגיאה
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }


    [HttpGet("GetGarageAvailableOptions")]
    public IActionResult GetGarageAvailableOptions([FromQuery] int? selectedgarage, [FromQuery] DateTime date)
    {
        try
        {
            int start = 8, end = 17;
            List<AvailableOptionsDTO> availableOptions = new List<AvailableOptionsDTO>();
            List<Appointment> appointments = context.Appointments.Where(a => a.GarageId == selectedgarage && a.AppointmentDate.Value.Date == date.Date).ToList();
                
            for (int time = start; time <= end; time++)
            {
                DateTime appTime = new DateTime(date.Year, date.Month, date.Day, time, 0, 0);
                if (!appointments.Exists(a => a.AppointmentDate == appTime))
                {
                    AvailableOptionsDTO adto = new AvailableOptionsDTO
                    {
                        OptionDate = appTime,
                        GarageId = selectedgarage
                    };
                    availableOptions.Add(adto);
                }
            }
            return Ok(availableOptions);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }




    [HttpPost("GetAppointmentsbyUser")]
    public IActionResult GetAppointmentsbyUser([FromBody] List<string> licenses)
    {
        try
        {
            List<AppointmentDTO> appointmentDTOs = new List<AppointmentDTO>();
            foreach (string L in licenses)
            {
                var vehicleExists = context.Vehicles.Any(v => v.LicensePlate == L);
                if (vehicleExists)
                {
                    foreach (var appointment in context.Appointments)
                    {
                        if (appointment.LicensePlate == L)
                        {
                            AppointmentDTO appointmentDTO = new AppointmentDTO(appointment);
                            appointmentDTOs.Add(appointmentDTO);
                        }
                    }
                }
            }
            return Ok(appointmentDTOs);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }







    [HttpPost("GenerateDefaultAppointments")]
    public IActionResult GenerateDefaultAppointments()
    {
        try
        {
            int daysToGenerate = 7; // כמה ימים קדימה ליצור תורים
            TimeSpan startHour = new TimeSpan(8, 0, 0);  // 08:00
            TimeSpan endHour = new TimeSpan(18, 0, 0);   // 18:00

            DateTime today = DateTime.Today;

            // שליפת כל המוסכים
            List<Garage> garages = context.Garages.ToList();

            foreach (Garage garage in garages)
            {
                for (int day = 0; day < daysToGenerate; day++)
                {
                    DateTime currentDate = today.AddDays(day);

                    for (TimeSpan hour = startHour; hour <= endHour; hour = hour.Add(TimeSpan.FromHours(1)))
                    {
                        DateTime optionDateTime = currentDate.Date + hour;

                        // לבדוק שלא נוצר כבר תור באותו זמן ומוסך
                        bool exists = context.AvailableOptions.Any(opt =>
                            opt.GarageId == garage.GarageId &&
                            opt.OptionDate == optionDateTime);

                        if (!exists)
                        {
                            AvailableOption newOption = new AvailableOption
                            {
                                GarageId = garage.GarageId,
                                OptionDate = optionDateTime
                            };

                            context.AvailableOptions.Add(newOption);
                        }
                    }
                }
            }

            context.SaveChanges();
            return Ok("Appointment options generated successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }




    [HttpPost("insertAppointments")]
    public IActionResult InsertAppointments([FromBody] List<TheGarageManagerServer.DTO.AppointmentDTO> appointments)
    {
        try
        {
            foreach (var appointmentDto in appointments)
            {
                var modelAppointment = appointmentDto.GetModels();

                // בדיקה האם כבר קיים תור לאותו רכב באותו תאריך
                bool alreadyExists = context.Appointments.Any(a =>
                    a.AppointmentDate == modelAppointment.AppointmentDate &&
                    a.LicensePlate == modelAppointment.LicensePlate);

                if (!alreadyExists)
                {
                    context.Appointments.Add(modelAppointment);
                }
            }

            context.SaveChanges();
            return Ok("Appointments inserted successfully (duplicates skipped).");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error while inserting appointments: {ex.Message}");
        }
    }

    [HttpPost("insertAppointment")]
    public IActionResult InsertAppointment([FromBody] TheGarageManagerServer.DTO.AppointmentDTO appointment, [FromQuery] string selectedGarage)
    {
        try
        {
            Garage? g = context.Garages.FirstOrDefault(gg => gg.RashamHavarot == selectedGarage);
            if (g == null)
            {
                return BadRequest("Garage not found.");
            }

            //check if vehicle exist
            Vehicle? v = context.Vehicles.FirstOrDefault(vv => vv.LicensePlate == appointment.LicensePlate);
            if (v == null && appointment.Vehicle == null)
            {
                return BadRequest("Vehicle not found and no vehicle data provided in appointment DTO.");
            }
            if (v == null && appointment.Vehicle != null)
            {
                v = appointment.Vehicle.GetVehicle();
                context.Vehicles.Add(v);
                context.SaveChanges();
            }
            var modelAppointment = appointment.GetModels();
            modelAppointment.GarageId = g.GarageId; // הגדרת ה-GarageId של הפגישה
            modelAppointment.AppointmentStatusId = 0; // הגדרת סטטוס ברירת מחדל (Pending)
            modelAppointment.AppointmentId = 0;
            
            context.Appointments.Add(modelAppointment);
            
            context.SaveChanges();
            return Ok("Appointments inserted successfully (duplicates skipped).");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error while inserting appointments: {ex.Message}");
        }
    }



    [HttpPost("insertVehicle")]
    public IActionResult InsertVehicle([FromBody] VehicleDTO vehicleDto)
    {
        try
        {
            if (vehicleDto == null)
            {
                return BadRequest("Vehicle data is missing.");
            }

            // לבדוק אם כבר קיים רכב עם אותו מספר רישוי
            bool exists = context.Vehicles.Any(v => v.LicensePlate == vehicleDto.LicensePlate);
            if (exists)
            {
                return Conflict("A vehicle with the same license plate already exists.");
            }

            Vehicle vehicle = vehicleDto.GetVehicle();
            context.Vehicles.Add(vehicle);
            context.SaveChanges();

            return Ok("Vehicle inserted successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error while inserting vehicle: {ex.Message}");
        }
    }


}