using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Models
{
    public class PatientDetails : Patient
    {
        public Guid Id { get; set; }
        public DateTime RecordCreationDate { get; set; }
    }
}
