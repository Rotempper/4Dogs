using _4DOG.Data.DTO;
using _4DOG.Data.Entities;
using _4DOG.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace _4DOG.Controllers
{
    [Route("api/DogOwner")]
    [ApiController]
    public class DogOwnerController : ControllerBase
    {
        private readonly DogOwnerServices _service;

        // בנאי
        public DogOwnerController(DogOwnerServices service)
        {
            _service = service;
        }

        // ----------- מקבל נתונים -------------
        [HttpGet]
        public ActionResult GetDogOwner() 
        {
            List<DogOwnerDTO> result = _service.GetDogOwneServices();
            return Ok(result);
        }

        // ------ מקבל נתונים ע"י מס מזהה ------
        [Route("{id}")]
        [HttpGet]
        public ActionResult GetDogOwnerId(int id) 
        {
            DogOwnerDTO result = _service.GetDogOwnerServicesId(id);
            return Ok(result);
        }

        // ------ מקבל אובייקט בעל הכלב ע"י מס מזהה של משתמש ------
        [Route("GetDogOwnerByUserid/{id}")]
        [HttpGet]
        public ActionResult GetDogOwnerByUserid(int id) 
        {
            DogOwner result = _service.getDogOwnerByUserId(id);
            return Ok(result);
        }

        // ------------- הוספה --------------------
        [HttpPost]
        public ActionResult AddDogOwner([FromBody] DogOwnerDTO user)
        {
            bool ok = _service.AddDogOwnerServices(user);
            if (ok)
            {
                return Created("", null);
            }
            return BadRequest();
        }

        // ---------------- מחיקה ---------------
        [Authorize(Roles = "Admin")]
        [Route("{id}")]
        [HttpDelete]
        public ActionResult DeleteDogOwner(int id)
        {
            ResponseDTO res = _service.DeleteDogOwnerServices(id);
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
        [Authorize(Roles = "DogOwnerRole")]
        [Route("{id}")]
        [HttpPut]
        public ActionResult UpdateDogOwner(int id, [FromBody] DogOwnerDTO user)
        {
            ResponseDTO res = _service.UpdateDogOwnerServices(id, user);
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
