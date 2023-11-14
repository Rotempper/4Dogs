using _4DOG.Data.DTO;
using _4DOG.Data.Entities;
using _4DOG.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace _4DOG.Controllers
{
    [Route("api/Training")]
    [ApiController]
    [AllowAnonymous]
    public class TrainingController : ControllerBase
    {
        private readonly TrainingServices _service;

        // בנאי
        public TrainingController(TrainingServices service)
        {
            _service = service;
        }

        // ----------- מקבל נתונים -------------
        [HttpGet]
        public ActionResult GetTrainings() 
        {
            List<TrainingDTO> result = _service.GetTrainingServices();
            return Ok(result);
        }

        // ------ מקבל נתונים ע"י מס מזהה ------
        [Route("{id}")]
        [HttpGet]
        public ActionResult GetTrainingId(int id) 
        {
            TrainingDTO result = _service.GetTrainingServicesId(id);
            return Ok(result);
        }

        //--- מקבל הזמנת מאלף ע"י מספר מזהה של משתמש ---
        [Route("GetDogTrainingDataId/{id}")]
        [HttpGet]
        public ActionResult GetDogTrainingDataId(int id)
        {
            List<TrainingDTO> result = _service.GetTrainingServicesIdData(id);
            return Ok(result);
        }

        // ------ מקבל מס מזהה של מאלף ע"י מס מזהה של המשתמש ------
        [Route("getTrainingIdByUserId/{userid}")]
        [HttpGet]
        public ActionResult getTrainingIdByUserId(int userid)
        {
            int id = _service.getTrainingIdByUserId(userid);
            return Ok(id);
        }

        // ------ מקבל אובייקט מאלף ע"י מס מזהה של משתמש ------
        [Route("getTrainingIdByUserIdObj/{userid}")]
        [HttpGet]
        public ActionResult getTrainingIdByUserIdObj(int userid)
        {
            Training result = _service.getTrainingIdByUserIdObj(userid);
            return Ok(result);
        }

        // ------------- הוספה  --------------------
        [HttpPost]
        public ActionResult AddTraining ([FromBody] TrainingDTO user)
        {
            bool ok = _service.AddTrainingServices(user);
            if (ok)
            {
                return Created("", null);
            }
            return BadRequest();
        }

        // ---------------- מחיקה  ---------------
        [Route("{id}")]
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteTraining (int id)
        {
            ResponseDTO res = _service.DeleteTrainingServices(id);
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
        [Authorize(Roles = "TrainingRole")]
        public ActionResult UpdateTraining(int id, [FromBody] TrainingDTO user)
        {
            ResponseDTO res = _service.UpdateTrainingServices(id, user);
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
