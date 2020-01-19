using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using AutoMapper;
using DataAccess.Contracts;

namespace DataAccess.Implementation
{
    public class PatientsRepository : IPatientsRepository
    {
        private readonly IMapper _mapper;
        private readonly PatientsDbContext _dbContext;

        public PatientsRepository(IMapper mapper, PatientsDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<Guid> AddPatient(Patient patient)
        {
            var patientRecord = _mapper.Map<PatientRecord>(patient);
            patientRecord.Id = Guid.NewGuid();
            patientRecord.RecordCreationDate = DateTime.UtcNow;
            await _dbContext.AddAsync(patientRecord);
            await _dbContext.SaveChangesAsync();
            return patientRecord.Id;
        }

        public Task<bool> DeletePatient(Guid Id)
        {
            var patientRecord = _dbContext.Patients.Find(Id);
            _dbContext.Patients.Remove(patientRecord);
            _dbContext.SaveChanges();
            return Task.FromResult<bool>(true);
        }

        public async Task<List<PatientDetails>> ListPatients(int page, int pageSize)
        {
            return _dbContext.Patients.Skip((page - 1) * pageSize).Take(pageSize).Select(x => _mapper.Map<PatientDetails>(x)).ToList();
        }
    }
}
