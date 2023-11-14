using _4DOG.Data.DTO;
using _4DOG.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace _4DOG.Controllers
{
    [Route("api/Shops")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ShopsController : ControllerBase
    {
        private readonly ShopsServices _service;

        // בנאי
        public ShopsController(ShopsServices service)
        {
            _service = service;
        }

        // ----------- מקבל נתונים -------------
        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetShopss() 
        {
            List<ShopsDTO> result = _service.GetShopsServices();
            return Ok(result);
        }

        // ------ מקבל נתונים ע"י מס מזהה ------
        [Route("{id}")]
        [HttpGet]
        public ActionResult GetShopsId(int id) 
        {
            ShopsDTO result = _service.GetShopsServicesId(id);
            return Ok(result);
        }

        // ------------- הוספה  --------------------
        [HttpPost]
        public ActionResult AddShops([FromBody] ShopsDTO user)
        {
            bool ok = _service.AddShopsServices(user);
            if (ok)
            {
                return Created("", null);
            }
            return BadRequest();
        }

        // ---------------- מחיקה  ---------------
        [Route("{id}")]
        [HttpDelete]
        public ActionResult DeleteShops(int id)
        {
            ResponseDTO res = _service.DeleteShopsServices(id);
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
        [Route("{id}")]
        [HttpPut]
        public ActionResult UpdateShops(int id, [FromBody] ShopsDTO user)
        {
            ResponseDTO res = _service.UpdateShopsServices(id, user);
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
