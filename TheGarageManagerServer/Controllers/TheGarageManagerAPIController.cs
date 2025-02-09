﻿using Microsoft.AspNetCore.Mvc;
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



}