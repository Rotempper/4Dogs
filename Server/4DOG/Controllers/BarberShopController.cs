using _4DOG.Data.DTO;
using _4DOG.Data.Entities;
using _4DOG.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace _4DOG.Controllers
{
    [Route("api/BarberShop")]
    [ApiController]
    [AllowAnonymous]
    public class BarberShopController : ControllerBase
    {
        private readonly BarberShopServices _service;

        //בנאי
        public BarberShopController(BarberShopServices service)
        {
            _service = service;
        }

        // ----------- מקבל נתונים -------------
        [HttpGet]
        public ActionResult GetUsers() 
        {
            List<BarberShopDTO> result = _service.GetBarberShopServices();
            return Ok(result);
        }

        // ------ מקבל נתונים ע"י מס מזהה ------
        [Route("{id}")]
        [HttpGet]
        public ActionResult GetBarberShopId(int id) 
        {
            BarberShopDTO result = _service.GetBarberShopServicesId(id);
            return Ok(result);
        }

        // ------ מקבל מס מזהה של מספרה ע"י מס מזהה של המשתמש ------
        [Route("getBarberShopIdByUserId/{userid}")]
        [HttpGet]
        public ActionResult getBarberShopIdByUserId(int userid)
        {
            int  id = _service.getBarberShopIdByUserId(userid);
            return Ok(id);
        }

        // ------ מקבל אובייקט מספרה ע"י מס מזהה של משתמש ------
        [Route("getBarberShopByUserIdObj/{userid}")]
        [HttpGet]
        public ActionResult getBarberShopByUserIdObj(int userid)
        {
            BarberShop result = _service.getBarberShopByUserIdObj(userid);
            return Ok(result);
        }

        // ------------- הוספה --------------------
        [HttpPost]
        public ActionResult AddBarberShop([FromBody] BarberShopDTO user)
        {
            bool ok = _service.AddBarberShopServices(user);
            if (ok)
            {
                return Created("", null);
            }
            return BadRequest();
        }

        // ---------------- מחיקה ---------------
        [Authorize(Roles = "Admin")]
        [Route("{id}")]
        [HttpDelete]
        public ActionResult DeleteBarberShop(int id)
        {
            ResponseDTO res = _service.DeleteBarberShopServices(id);
            if (res.Status == Data.DTO.StatosCode.Error)
            {
                return BadRequest(res);
            }
            else
            {
                return Ok(res);
            }
        }

        // -------------- עדכון  ---------------
        [Authorize(Roles = "BarberShopRole")]
        [Route("{id?}")]
        [HttpPut]
        public ActionResult UpdateBarberShop(int id, [FromBody] BarberShopDTO user)
        {
            ResponseDTO res = _service.UpdateBarberShopServices(id, user);
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
