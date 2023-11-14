using _4DOG.Data.DTO;
using _4DOG.Data.Entities;
using _4DOG.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace _4DOG.Controllers
{
    [Route("api/TypesHaircut")]
    [ApiController]
    public class TypesHaircutController:ControllerBase
    {
        private readonly TypesHaircutServices _service;

        // בנאי
        public TypesHaircutController(TypesHaircutServices service)
        {
            _service = service;
        }

        // ----------- מקבל נתונים -------------
        [HttpGet]
        public ActionResult GetTypesHaircut() // מתודה
        {
            List<TypesHaircutDTO> result = _service.GetTypesHaircutServices();
            return Ok(result);
        }

        // ------ מקבל נתונים ע"י מס מזהה ------
        [Route("{id}")]
        [HttpGet]
        public ActionResult GetTypesHaircutId(int id) // מתודה
        {
            TypesHaircutDTO result = _service.GetTypesHaircutServicesId(id);
            return Ok(result);
        }

        // ------ מקבל סוג תספורת ע"י מס מזהה של מספרה ------
        [Route("getTypesHaircutByBarberShopId/{barberShopId}")]
        [HttpGet]
        public ActionResult getTypesHaircutByBarberShopId(int barberShopId)
        {
            TypesHaircut result = _service.getTypesHaircutByBarberShopId(barberShopId);
            return Ok(result);
        }

        // ------ מקבל אובייקט תספורות ע"י מס מזהה ------
        [Route("getTypesHaircutById1/{id}")]
        [HttpGet]
        public ActionResult getTypesHaircutById1(int id)
        {
            TypesHaircut result = _service.getTypesHaircutById1(id);
            return Ok(result);
        }

        // ------ מקבל תספורת ע"י מס מזהה ------
        [Route("getTypesHaircutById2/{id}")]
        [HttpGet]
        public ActionResult getTypesHaircutById2(int id)
        {
            int ID = _service.getTypesHaircutById2(id);
            return Ok(ID);
        }

        // ------------- הוספה  --------------------
        [HttpPost]
        public ActionResult AddTypesHaircut([FromBody] TypesHaircutDTO user)
        {
            bool ok = _service.AddTypesHaircutServices(user);
            if (ok)
            {
                return Created("", null);
            }
            return BadRequest();
        }

        // ---------------- מחיקה  ---------------
        [Route("{id}")]
        [HttpDelete]
        public ActionResult DeleteTypesHaircut(int id)
        {
            ResponseDTO res = _service.DeleteTypesHaircutServices(id);
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
        [Route("{id?}")]
        [HttpPut]
        public ActionResult UpdateTypesHaircut(int id, [FromBody] TypesHaircutDTO user)
        {
            ResponseDTO res = _service.UpdateTypesHaircutServices(id, user);
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
