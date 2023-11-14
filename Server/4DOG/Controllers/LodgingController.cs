using _4DOG.Data.DTO;
using _4DOG.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace _4DOG.Controllers
{
    [Route("api/Lodging")]
    [ApiController]
    [Authorize]

    public class LodgingController : ControllerBase
    {
        private readonly LodgingServices _service;

        // בנאי
        public LodgingController(LodgingServices service)
        {
            _service = service;
        }

        // ----------- מקבל נתונים -------------
        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetLodgings()
        {
            List<LodgingDTO> result = _service.GetLodgingServices();
            return Ok(result);
        }

        // ------ מקבל נתונים ע"י מס מזהה ------
        [Route("{id}")]
        [HttpGet]
        public ActionResult GetLodgingId(int id)
        {
            LodgingDTO result = _service.GetLodgingServicesId(id);
            return Ok(result);
        }

        //--- מקבל הזמנת לינה ע"י מספר מזהה של משתמש ---
        [Route("getLodgingByUserId/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetDogTrainingServicesId(int id)
        {
            List<LodgingDTO> result = _service.getLodgingByUserId(id);
            return Ok(result);
        }

        // ------------- הוספה  --------------------
        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddLodging([FromBody] LodgingDTO user)
        {
            bool ok = _service.AddLodgingServices(user);
            if (ok)
            {
                return Created("", null);
            }
            return BadRequest();
        }

        // ---------------- מחיקה  ---------------
        [Route("{id}")]
        [HttpDelete]
        public ActionResult DeleteLodging(int id)
        {
            ResponseDTO res = _service.DeleteLodgingServices(id);
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
        public ActionResult UpdateLodging(int id, [FromBody] LodgingDTO user)
        {
            ResponseDTO res = _service.UpdateLodgingServices(id, user);
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
