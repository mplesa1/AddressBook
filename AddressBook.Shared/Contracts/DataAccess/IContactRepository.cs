using AddressBook.Model;
using AddressBook.Shared.DataTransferObjects.Contact;
using AddressBook.Shared.Infrastructure.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddressBook.Shared.Contracts.DataAccess
{
    public interface IContactRepository
    {
        Task<ICollection<Contact>> FindAllAsync();

        Task CreateAsync(Contact contact);

        Task UpdateAsync(Contact contact);

        Task<Contact> FindByIdAsync(int id);

        Task DeleteAsync(Contact contact);

        Task<PagedResult<ContactPaginationDto>> GetPagedResultAsync(ContactPagingRequest contactPagingRequest);

        Task<Contact> FindByNameOrAddressAsync(string name, string address);

        Task<bool> IsExistByNameOrAddressAndContactIdAsync(string name, string address, int contactId);
    }
}
