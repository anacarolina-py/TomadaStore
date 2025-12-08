using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TomadaStore.Models.DTOs.Sale
{
    public class SaleProductDTO
    {
        [Required]
        public string ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
