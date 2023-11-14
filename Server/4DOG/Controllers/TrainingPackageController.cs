using _4DOG.Data.DTO;
using _4DOG.Data.Entities;
using _4DOG.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace _4DOG.Controllers
{
    [Route("api/TrainingPackage")]
    [ApiController]
    public class TrainingPackageController:ControllerBase
    {
        private readonly TrainingPackageServices _service;

        // בנאי
        public TrainingPackageController(TrainingPackageServices service)
        {
            _service = service;
        }

        // ----------- מקבל נתונים -------------
        [HttpGet]
        public ActionResult GetTrainingPackage() 
        {
            List<TrainingPackageDTO> result = _service.GetTrainingPackageServices();
            return Ok(result);
        }

        // ------ מקבל נתונים ע"י מס מזהה ------
        [Route("{id?}")]
        [HttpGet]
        public ActionResult GetTrainingPackageId(int id) 
        {
            TrainingPackageDTO result = _service.GetTrainingPackageServicesId(id);
            return Ok(result);
        }

        // ------ מקבל חבילת אילוף ע"י מס מזהה של אילוף ------
        [Route("getTrainingPackageByTrainingId/{trainingId}")]
        [HttpGet]
        public ActionResult getTrainingPackageByTrainingId(int trainingId)
        {
            TrainingPackage result = _service.getTrainingPackageByTrainingId(trainingId);
            return Ok(result);
        }

        // ------------- הוספה  --------------------
        [HttpPost]
        public ActionResult AddTrainingPackage([FromBody] TrainingPackageDTO user)
        {
            bool ok = _service.AddTrainingPackageServices(user);
            if (ok)
            {
                return Created("", null);
            }
            return BadRequest();
        }

        // ---------------- מחיקה  ---------------
        [Route("{id}")]
        [HttpDelete]
        public ActionResult DeleteTrainingPackage(int id)
        {
            ResponseDTO res = _service.DeleteTrainingPackageServices(id);
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
        public ActionResult UpdateTrainingPackage(int id, [FromBody] TrainingPackageDTO user)
        {
            ResponseDTO res = _service.UpdateTrainingPackageServices(id, user);
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
