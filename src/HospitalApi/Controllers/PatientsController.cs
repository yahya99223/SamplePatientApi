using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using DataAccess.Contracts;
using HospitalApi.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

namespace HospitalApi.Controllers
{
    [Route("v{version:apiVersion}/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientsRepository _patientsRepository;
        public PatientsController(IPatientsRepository patientsRepository)
        {
            _patientsRepository = patientsRepository;
        }
        [HttpGet]
        public async Task<ActionResult<List<PatientDetails>>> ListPatients([FromQuery] PagingParameters pagingParameters)
        {
            var patients = _patientsRepository.ListPatients(pagingParameters.Page, pagingParameters.PageSize);
            if (patients == null || !patients.Any())
                return NoContent();
            return Ok(patients);
        }
        [HttpPost("patient")]
        [PatientValidationFilter]
        public async Task<ActionResult<Guid>> PostPatient([FromBody] Patient patient)
        {
            var id = _patientsRepository.AddPatient(patient);
            return StatusCode(201, new { Id = id });
        }
        [HttpDelete("{patientId}")]
        public async Task<ActionResult> DeletePatient([FromRoute] Guid patientId)
        {
            var success = _patientsRepository.DeletePatient(patientId);
            if (!success)
                return NotFound();
            return Ok();
        }
        [HttpGet("{patientId}")]
        public async Task<ActionResult> GetPatient([FromRoute] Guid patientId)
        {
            var patient = _patientsRepository.GetPatient(patientId);
            if (patient == null)
                return NotFound();
            return Ok(patient);
        }
    }
}
