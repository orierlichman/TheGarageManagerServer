using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using TheGarageManagerServer.DTO;
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
    public IActionResult Register([FromBody] UserDTO userDto)
    {
        try
        {
            HttpContext.Session.Clear(); //Logout any previous login attempt

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


    //[HttpPost("addAppointment")]
    //public IActionResult AddAppointment([FromBody] AppointmentDTO appointmentDto)
    //{
    //    try
    //    {
    //        // יצירת אובייקט Appointment ממודל ה-DTO
    //        Appointment modelsAppointment = appointmentDto.GetModels();

    //        // סטטוס ברירת מחדל הוא "Pending"
    //        modelsAppointment.AppointmentStatusId = 0; // 0 הוא סטטוס Pending לפי הטבלה שלך

    //        // הוספת הפגישה למסד הנתונים
    //        context.Appointments.Add(modelsAppointment);
    //        context.SaveChanges();

    //        // החזרת המידע המפוקס מחדש ב-DTO
    //        AppointmentDTO dtoAppointment = new AppointmentDTO(modelsAppointment);
    //        return Ok(dtoAppointment);
    //    }
    //    catch (Exception ex)
    //    {
    //        // במקרה של שגיאה
    //        return BadRequest(ex.Message);
    //    }
    //}


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
            // שליפת כל הבקשות מהמסד נתונים (Appointments)
            var appointments = context.Appointments
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






}