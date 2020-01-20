using System;
using Shared.Models.enums;

namespace DataAccess.Contracts
{
    public class PatientRecord
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public int FileNo { get; set; }
        public string CitizenId { get; set; }

        public DateTime Birthdate { get; set; }

        public Gender Gender { get; set; }

        public bool Vip { get; set; } = false;

        public string Nationality { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string ContactPerson { get; set; }

        public string ContactRelation { get; set; }

        public string ContactPhone { get; set; }

        public string Image { get; set; }//todo

        public DateTime FirstVisitDate { get; set; }//todo
        public DateTime RecordCreationDate { get; set; }
    }
}