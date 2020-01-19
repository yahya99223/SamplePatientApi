using Api.Models;
using AutoMapper;
using DataAccess.Contracts;

namespace DataAccess.Implementation
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Patient, PatientRecord>();
            CreateMap<PatientRecord, Patient>();
            CreateMap<PatientDetails, PatientRecord>();
            CreateMap<PatientRecord, PatientDetails>();
        }
    }
}
