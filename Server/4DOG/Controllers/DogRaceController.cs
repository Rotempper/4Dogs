using _4DOG.Data.DTO;
using _4DOG.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace _4DOG.Controllers
{
    [Route("api/DogRace")]
    [ApiController]
    public class DogRaceController : ControllerBase
    {
        private readonly DogRaceServices _service;

        //בנאי
        public DogRaceController(DogRaceServices service)
        {
            _service = service;
        }

        // ----------- מקבל נתונים -------------
        [HttpGet]
        public ActionResult GetDogRace()
        {
            List<DogRaceDTO> result = _service.GetDogRaceServices();
            return Ok(result);
        }

        // ------ מקבל נתונים ע"י מס מזהה ------
        [Route("{id}")]
        [HttpGet]
        public ActionResult GetDogRaceId(int id) 
        {
            DogRaceDTO result = _service.GetDogRaceServicesId(id);
            return Ok(result);
        }

        // ------------- הוספה  --------------------
        [HttpPost]
        public ActionResult AddDogRace([FromBody] DogRaceDTO user)
        {
            bool ok = _service.AddDogRaceServices(user);
            if (ok)
            {
                return Created("", null);
            }
            return BadRequest();
        }

        // ---------------- מחיקה  ---------------
        [Route("{id}")]
        [HttpDelete]
        public ActionResult DeleteDogRace(int id)
        {
            ResponseDTO res = _service.DeleteDogRaceServices(id);
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
        public ActionResult UpdateDogRace(int id, [FromBody] DogRaceDTO user)
        {
            ResponseDTO res = _service.UpdateDogRaceServices(id, user);
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
