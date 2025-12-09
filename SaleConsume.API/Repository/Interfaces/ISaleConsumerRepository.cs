using TomadaStore.Models.DTOs.Sale;
using TomadaStore.Models.Models;

namespace SaleConsume.API.Repository.Interfaces
{
    public interface ISaleConsumerRepository
    {
        Task SaveSaleAsync(Sale sale);
    }
}
