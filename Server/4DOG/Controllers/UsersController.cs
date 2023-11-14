
using _4DOG.Data.DTO;
using _4DOG.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace _4DOG.Controllers
{
    [Route("api/Users")]
    [ApiController]
    [Authorize]
    public class UsersController:ControllerBase
    {
        private readonly UsersServices _service;      
        private readonly JwtService _m_jwtService; //Token

        // בנאי
        public UsersController(UsersServices service, JwtService m_jwtService)
        {
            _service = service;
            _m_jwtService = m_jwtService;
        }

        // ----------- מקבל נתונים -------------
        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetUsers()
        {
            List<UsersDTO> result = _service.GetUsersServices();
            return Ok(result);
        }

        // ------ מקבל נתונים ע"י מס מזהה ------
        [Route("{id}")]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetUserId(int id)
        {
            UsersDTO result = _service.GetUserServicesId(id);
            return Ok(result);
        }

        // ------ מקבל שם משתמש ע"י מס מזהה ------
        [Route("getUserNameById/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetDogTrainingServicesId(int id)
        {
            UsersDTO result = _service.getUserNameById(id);
            return Ok(result);
        }

        // ------ מקבל מס מזהה של משתמש ע"י מייל ------
        [Route("getUserIdByEmail/{Email}")]
        [AllowAnonymous]
        [HttpGet]
        public ActionResult getUserIdByEmail(string Email)
        {
          int id = _service.getUserIdByEmail(Email);
            return Ok(id);
        }

        // ------ מקבל מייל ובודק אם קיים במערכת ------
        [Route("checkEmailInSql/{Email}")]
        [AllowAnonymous]
        [HttpGet]
        public ActionResult checkEmailInSql(string Email)
        {
            bool result = _service.checkEmailInSql(Email);
            return Ok(result);
        }

        // ------------- הוספת משתמש - מייל וסיסמה --------------------
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Add([FromBody] UsersDTO user)
        {
            if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
                return BadRequest("שדות אימייל וסיסמה הינם חובם");
            bool Ok = _service.AddUserServices(user);
            if (Ok)
               return Created("", null);
            throw new Exception("Problem when trying add user to db");
        }

        // שליחת שם משתמש וסיסמה מסוג post-מכניס 
        // מכיון שאנחנו לא רוצים שהנתונים של המשתמש יעברו בצורה גלויה בדפדפן

        [Route("auth")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Auth([FromBody] AuthRequestDTO request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(" .חובה להזין אימייל וסיסמא");
            }
            // בדיקה האם המשתמש נמצא במאגר הנתונים בעזרת השירות - סרוויס
            UsersDTO userFoundInDb = _service.GetUserServicesIdGWT(request.Email, request.Password);
            if (userFoundInDb != null)
            {
                string jwtToken = _m_jwtService.GenerateToken(userFoundInDb.Id.ToString(), userFoundInDb.Role);
                
                return Ok(new
                {
                    Id = userFoundInDb.Id,
                    Token = jwtToken,
                    Role = userFoundInDb.Role
                });                             
            }
            return Unauthorized(".משתמש אינו קיים"); // מחזיר 401
        }

        // ---------------- מחיקה משתמש ---------------
        [Authorize(Roles = "Admin")]
        [Route("{id}")]
        [HttpDelete]
        public ActionResult DeleteUser(int id)
        {
            ResponseDTO res = _service.DeleteUserServices(id);
            if (res.Status == Data.DTO.StatosCode.Error)
            {
                return BadRequest(res);
            }
            else
            {
                return Ok(res);
            }
        }

        // -------------- עדכון משתמש ---------------
        [Route("{id}")]
        [HttpPut] 
        public ActionResult UpdateUser(int id, [FromBody] UsersDTO user)
        {
            ResponseDTO res = _service.UpdateUserServices(id, user);
            if (res.Status == Data.DTO.StatosCode.Error)
            {
                return BadRequest(res);
            }
            else
            {
                return Ok(res);
            }
        }       
    }
}
