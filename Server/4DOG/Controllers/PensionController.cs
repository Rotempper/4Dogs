using _4DOG.Data.DTO;
using _4DOG.Data.Entities;
using _4DOG.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace _4DOG.Controllers
{
    [Route("api/Pension")]
    [ApiController]
    [AllowAnonymous]

    public class PensionController : ControllerBase
    {
        private readonly PensionServices _service;

        // בנאי
        public PensionController(PensionServices service)
        {
            _service = service;
        }

        // ----------- מקבל נתונים -------------
        [HttpGet]
        public ActionResult GetPension()
        {
            List<PensionDTO> result = _service.GetPensionServices();
            return Ok(result);
        }

        // ------ מקבל נתונים ע"י מס מזהה ------
        [Route("{id}")]
        [HttpGet]
        public ActionResult GetPensionyId(int id)
        {
            PensionDTO result = _service.GetPensionServicesId(id);
            return Ok(result);
        }

        //--- מקבל פרטי פנסיון ע"י מספר מזהה של משתמש ---
        [Route("getPensionByUserId/{userid}")]
        [HttpGet]
        public ActionResult getPensionByUserId(int userid)
        {
            Pension result = _service.getPensionByUserId(userid);
            return Ok(result);
        }

        // ------------- הוספה  --------------------
        [HttpPost]
        public ActionResult AddPension([FromBody] PensionDTO user)
        {
            bool ok = _service.AddPensionServices(user);
            if (ok)
            {
                return Created("", null);
            }
            return BadRequest();
        }

        // ---------------- מחיקה  ---------------
        [Authorize(Roles = "Admin")]
        [Route("{id}")]
        [HttpDelete]
        public ActionResult DeletePension(int id)
        {
            ResponseDTO res = _service.DeletPensionServices(id);
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
        [Authorize(Roles = "PensionRole")]
        [Route("{id}")]
        [HttpPut]
        public ActionResult UpdatePension(int id, [FromBody] PensionDTO user)
        {
            ResponseDTO res = _service.UpdatePensionServices(id, user);
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
