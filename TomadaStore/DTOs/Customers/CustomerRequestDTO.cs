using System;
using System.Collections.Generic;
using System.Text;

namespace TomadaStore.Models.DTOs.Customer
{
    public class CustomerRequestDTO
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string? PhoneNumber { get; init; }
       
    }
}
