using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using tut10.DTOs.Requests;
using tut10.Exceptions;
using tut10.Services;

namespace tut10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IClinicDbService _service;

        public DoctorController(IClinicDbService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetDoctors()
        {
            return Ok(_service.GetDoctors());
        }
            
        [HttpPost]
        public IActionResult AddDoctor(AddDoctorRequest request)
        {
            try
            {
                var doctor = _service.AddDoctor(request);
                return CreatedAtAction("Add Doctor", doctor);
            }
            catch (DoctorAlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDoctor(string id)
        {
            try
            {
                _service.DeleteDoctor(id);
                return Ok();
            }
            catch (DoctorNotFound ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}")]
        public IActionResult ModifyDoctor(ModifyDoctorRequest request)
        {
            try
            {
               var modifiedDoctor = _service.ModifyDoctor(request);
                return CreatedAtAction("Modify Doctor", modifiedDoctor);

            }
            catch (DoctorNotFound ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
