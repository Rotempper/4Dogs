using _4DOG.Data.DTO;
using _4DOG.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace _4DOG.Controllers
{
    [Route("api/Haircuts")]
    [ApiController]
    [Authorize]
    public class HaircutsController : ControllerBase
    {
        private readonly HaircutsServices _service;

        // בנאי
        public HaircutsController(HaircutsServices service)
        {
            _service = service;
        }

        // ----------- מקבל נתונים -------------
        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetHaircuts() 
        {
            List<HaircutsDTO> result = _service.GetHaircutsServices();
            return Ok(result);
        }

        // ------ מקבל נתונים ע"י מס מזהה ------
        [Route("{id}")]
        [HttpGet]
        public ActionResult GetHaircutsId(int id)
        {
            HaircutsDTO result = _service.GetHaircutsServicesId(id);
            return Ok(result);
        }

        //--- מקבל הזמנת תספורת ע"י מספר מזהה של משתמש ---
        [Route("getHaircutsDTOByUserId/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult getHaircutsByUserIdServices(int id)
        {
            List<HaircutsDTO> result = _service.getHaircutsDTOByUserId(id);
            return Ok(result);
        }

        // ------------- הוספה  --------------------
        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddHaircuts([FromBody] HaircutsDTO user)
        {
            bool ok = _service.AddHaircutsServices(user);
            if (ok)
            {
                return Created("", null);
            }
            return BadRequest();
        }

        // ------ הוספת קביעת שעה להזמנה ------
        [Route("checkTime")]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult checkTime([FromBody] HaircutsDTO obj)
        {
            return Ok(_service.checkTime(obj));
        }

        // ---------------- מחיקה  ---------------
        [Route("{id}")]
        [HttpDelete]
        public ActionResult DeleteHaircuts(int id)
        {
            ResponseDTO res = _service.DeleteHaircutsServices(id);
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
        public ActionResult UpdateHaircuts(int id, [FromBody] HaircutsDTO user)
        {
            ResponseDTO res = _service.UpdateHaircutsServices(id, user);
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
