using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PetShop.Application.DTO
{
    public class CnpjResponseDto
    {
        public string RegistrationNumber { get; set; }

        public string CompanyName { get; set; }

        public string TradeName { get; set; }

        public string DescriptionRegistrationStatus { get; set; }

        public int PostalCode { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string PhoneNumber { get; set; }

        public int ShareCapital { get; set; }


        public string DescriptionSize { get; set; }

    }
}
