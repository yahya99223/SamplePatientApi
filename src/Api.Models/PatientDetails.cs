using System;

namespace Api.Models
{
    public class PatientDetails : Patient
    {
        public Guid Id { get; set; }
        public DateTime RecordCreationDate { get; set; }
    }
}
