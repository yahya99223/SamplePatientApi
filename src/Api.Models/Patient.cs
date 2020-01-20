using System;
using Shared.Models.enums;

namespace Api.Models
{
    public class Patient
    {
        public string Name { get; set; }

        public string FileNo { get; set; }
        public string CitizenId { get; set; }

        public string Birthdate { get; set; }

        public string Gender { get; set; }

        public string Vip { get; set; }

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

        public string FirstVisitDate { get; set; }//todo
    }
}