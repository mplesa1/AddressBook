using AddressBook.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddressBook.Shared.Contracts.DataAccess
{
    public interface ISettlementRepository
    {
        Task<ICollection<Settlement>> FindAllAsync();

        Task CreateAsync(Settlement settlement);

        Task UpdateAsync(Settlement settlement);

        Task<Settlement> FindByIdAsync(int id);

        Task DeleteAsync(Settlement city);
    }
}
