using _4DOG.Data.DTO;
using _4DOG.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace _4DOG.Controllers
{
    [Route("api/Citys")]
    [ApiController]
    public class CitysController:ControllerBase
    {
        private readonly CitysServices _service;

        //בנאי
        public CitysController(CitysServices service)
        {
            _service = service;
        }

        // ----------- מקבל נתונים -------------
        [HttpGet]
        public ActionResult GetCity() // מתודה
        {
            List<CitysDTO> result = _service.GetCitysServices();
            return Ok(result);
        }

        // ------ מקבל נתונים ע"י מס מזהה ------
        [Route("{id}")]
        [HttpGet]
        public ActionResult GetCityId(int id) // מתודה
        {
            CitysDTO result = _service.GetCitysServicesId(id);
            return Ok(result);
        }

        // ------------- הוספה  --------------------
        [HttpPost]
        public ActionResult AddCity([FromBody] CitysDTO user)
        {
            bool ok = _service.AddCitysServices(user);
            if (ok)
            {
                return Created("", null);
            }
            return BadRequest();
        }

        // ---------------- מחיקה ---------------
        [Route("{id}")]
        [HttpDelete]
        public ActionResult DeleteCity(int id)
        {
            ResponseDTO res = _service.DeleteCitysServices(id);
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
        public ActionResult UpdateCity(int id, [FromBody] CitysDTO user)
        {
            ResponseDTO res = _service.UpdateCityServices(id, user);
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
