using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;

namespace DataAccess.Contracts
{
    public interface IPatientsRepository
    {
        Task<Guid> AddPatient(Patient patient);
        Task<bool> DeletePatient(Guid Id);
        Task<List<PatientDetails>> ListPatients(int page, int pageSize);
    }
}
