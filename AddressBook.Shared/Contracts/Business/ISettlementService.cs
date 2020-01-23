using AddressBook.Shared.DataTransferObjects.Settlement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddressBook.Shared.Contracts.Business
{
    public interface ISettlementService
    {
        Task<ICollection<SettlementDto>> FindAllAsync();

        Task CreateAsync(CreateSettlementDto dto);

        Task UpdateAsync(int settlementId, CreateSettlementDto dto);

        Task DeleteAsync(int settlementId);
    }
}
