using System;
using System.Collections.Generic;
using System.Text;

namespace TomadaStore.Models.DTOs.Customer
{
    public class CustomerResponseDTO
    {
        public int Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string? PhoneNumber { get; init; }
        public bool IsActive { get; init; }
    }
}
