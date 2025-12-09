using System;
using System.Collections.Generic;
using System.Text;

namespace TomadaStore.Models.DTOs.Sale
{
    public class SaleRequestDTO
    {
        [Required]
        public List<SaleProductDTO> Products { get; set; }

    }
}
