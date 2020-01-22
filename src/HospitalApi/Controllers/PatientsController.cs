using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using HospitalApi.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Service.Contracts;

namespace HospitalApi.Controllers
{
    [Route("v{version:apiVersion}/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientsService _patientsService;
        public PatientsController(IPatientsService patientsService)
        {
            _patientsService = patientsService;
        }
        [HttpGet]
        public async Task<ActionResult<List<PatientDetails>>> ListPatients([FromQuery] PagingParameters pagingParameters)
        {
            var patients = _patientsService.ListPatients(pagingParameters.Page, pagingParameters.PageSize);
            if (patients == null || !patients.Any())
                return NoContent();
            return Ok(patients);
        }
        [HttpPost("patient")]
        [PatientValidationFilter]
        public async Task<ActionResult<Guid>> PostPatient([FromBody] Patient patient)
        {
            var id = _patientsService.AddPatient(patient);
            return StatusCode(201, new { Id = id });
        }

        [HttpGet("{patientId}")]
        public async Task<ActionResult> GetPatient([FromRoute] Guid patientId)
        {
            var patient = _patientsService.GetPatient(patientId);
            if (patient == null)
                return NotFound();
            return Ok(patient);
        }

        [HttpDelete("{patientId}")]
        public async Task<ActionResult> DeletePatient([FromRoute] Guid patientId)
        {
            var success = _patientsService.DeletePatient(patientId);
            if (!success)
                return NotFound();
            return Ok();
        }

        [HttpPost("{patientId}/photo")]
        public async Task<ActionResult> PostPatientImage([FromRoute] Guid patientId, IFormFile image)
        {
            if (image == null)
                return BadRequest(new ErrorResponse(nameof(image),"Please submit a file image with name image"));
            var imageStream = image.OpenReadStream();
            var result = _patientsService.AttachImage(patientId, imageStream);
            if (!result)
                return NotFound();
            return Ok();
        }

        [HttpGet("{patientId}/photo")]
        public async Task<ActionResult> GetPatientImage([FromRoute] Guid patientId)
        {
            var result = _patientsService.GetPatientImage(patientId);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
