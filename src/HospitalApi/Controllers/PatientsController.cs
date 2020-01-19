using System;
using System.Collections.Generic;
using Api.Models;
using DataAccess.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApi.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientsRepository _patientsRepository;
        public PatientsController(IPatientsRepository patientsRepository)
        {
            _patientsRepository = patientsRepository;
        }
        [HttpGet]
        public ActionResult<List<PatientDetails>> ListPatients([FromQuery] PagingParameters pagingParameters)
        {
            return Ok(_patientsRepository.ListPatients(pagingParameters.Page, pagingParameters.PageSize).Result);
        }
        [HttpPost("patient")]
        public ActionResult PostPatient([FromBody] Patient patient)
        {
            _patientsRepository.AddPatient(patient);
            return StatusCode(201);
        }
        [HttpDelete("{patientId}")]
        public ActionResult DeletePatient([FromRoute] Guid patientId)
        {
            _patientsRepository.DeletePatient(patientId);
            return Ok();
        }

    }
}
