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
        public Guid AddPatient(Patient patient)
        {
            var patientRecord = _mapper.Map<PatientRecord>(patient);
            patientRecord.Id = Guid.NewGuid();
            patientRecord.RecordCreationDate = DateTime.UtcNow;
            _dbContext.Add(patientRecord);
            _dbContext.SaveChanges();
            return patientRecord.Id;
        }

        public bool DeletePatient(Guid id)
        {
            var patientRecord = _dbContext.Patients.Find(id);
            if (patientRecord == null)
                return false;
            _dbContext.Patients.Remove(patientRecord);
            _dbContext.SaveChanges();
            return true;
        }

        public List<PatientDetails> ListPatients(int page, int pageSize)
        {
            return _dbContext.Patients.Skip((page - 1) * pageSize).Take(pageSize)
                .Select(x => _mapper.Map<PatientDetails>(x)).ToList();
        }

        public PatientDetails GetPatient(Guid id)
        {
            var patient = _dbContext.Patients.FirstOrDefault(x => x.Id == id);
            if (patient == null)
                return null;
            return _mapper.Map<PatientDetails>(patient);
        }
    }
}
