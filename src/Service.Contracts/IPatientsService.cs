using System;
using System.Collections.Generic;
using System.IO;
using Api.Models;

namespace Service.Contracts
{
    public interface IPatientsService
    {
        Guid AddPatient(Patient patient);
        bool DeletePatient(Guid id);
        bool AttachImage(Guid patientId, Stream file);
        Stream GetPatientImage(Guid patientId);
        List<PatientDetails> ListPatients(int page, int pageSize);
        PatientDetails GetPatient(Guid id);
    }
}
