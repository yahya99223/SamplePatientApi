using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;

namespace DataAccess.Contracts
{
    public interface IPatientsRepository
    {
        Guid AddPatient(Patient patient);
        bool DeletePatient(Guid id);
        List<PatientDetails> ListPatients(int page, int pageSize);
        PatientDetails GetPatient(Guid id);
    }
}
