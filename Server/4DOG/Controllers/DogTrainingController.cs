using _4DOG.Data.DTO;
using _4DOG.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace _4DOG.Controllers
{
    [Route("api/DogTraining")]
    [ApiController]
    public class DogTrainingController : ControllerBase
    {
        private readonly DogTrainingServices _service;

        // בנאי
        public DogTrainingController(DogTrainingServices service)
        {
            _service = service;
        }

        // ----------- מקבל נתונים -------------
        [HttpGet]
        public ActionResult GetDogTrainings() // מתודה
        {
            List<DogTrainingDTO> result = _service.GetDogTrainingServices();
            return Ok(result);
        }

        // ------ מקבל נתונים ע"י מס מזהה ------
        [Route("{id}")]
        [HttpGet]
        public ActionResult GetDogTrainingId(int id) // מתודה
        {
            DogTrainingDTO result = _service.GetDogTrainingServicesId(id);
            return Ok(result);
        }

        // ------ מקבל רשימה של מאלף כלבים ע"י מס מזהה של המשתמש ------
        [Route("GetDogTrainingServicesId/{id}")]
        [HttpGet]
        public ActionResult GetDogTrainingServicesId(int id)
        {
            List<DogTrainingDTO> result = _service.gettDogTrainingByUserId(id);
            return Ok(result);
        }
     
        // ------------- הוספה  --------------------
        [HttpPost]
        public ActionResult AddDogTraining([FromBody] DogTrainingDTO user)
        {
            bool ok = _service.AddDogTrainingServices(user);
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
        public ActionResult DeleteDogTraining(int id)
        {
            ResponseDTO res = _service.DeleteDogTrainingServices(id);
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
        public ActionResult UpdateDogTraining(int id, [FromBody] DogTrainingDTO user)
        {
            ResponseDTO res = _service.UpdateDogTrainingServices(id, user);
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
