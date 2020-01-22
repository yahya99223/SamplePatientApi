using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Api.Models;
using AutoMapper;
using DataAccess.Contracts;
using DataAccess.Implementation;
using Microsoft.Extensions.Options;
using Service.Contracts;
using Shared.Models.Settings;

namespace Service.Implementation
{
    public class PatientsService : IPatientsService
    {
        private readonly IMapper _mapper;
        private readonly PatientsDbContext _dbContext;
        private readonly IStorageHandler _storageHandler;
        private readonly AppSettings _settings;

        public PatientsService(IStorageHandler storageHandler, IMapper mapper, IOptions<Settings> settings, PatientsDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _storageHandler = storageHandler;
            _settings = settings.Value.AppSettings;
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

        public bool AttachImage(Guid patientId, Stream file)
        {
            var patient = _dbContext.Patients.FirstOrDefault(x => x.Id == patientId);
            if (patient == null)
                return false;
            var url = _storageHandler.StoreFile(file);
            patient.Image = url;
            _dbContext.SaveChanges();
            return true;
        }

        public Stream GetPatientImage(Guid patientId)
        {
            var patient = _dbContext.Patients.FirstOrDefault(x => x.Id == patientId);
            if (patient == null || string.IsNullOrEmpty(patient.Image))
                return null;
            var image = _storageHandler.RetrieveFile(patient.Image);
            return image;
        }

        public List<PatientDetails> ListPatients(int page, int pageSize)
        {
            List<PatientDetails> resultPatients = new List<PatientDetails>();
            var patients = _dbContext.Patients.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            foreach (var patient in patients)
            {
                var tempPatient = _mapper.Map<PatientDetails>(patient);
                if (!string.IsNullOrEmpty(patient.Image))
                    tempPatient.Photo = $"{_settings.BasePath}/patients/{patient.Id}/photo";
                resultPatients.Add(tempPatient);
            }
            return resultPatients;
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
