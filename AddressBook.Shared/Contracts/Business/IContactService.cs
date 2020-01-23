using AddressBook.Shared.DataTransferObjects.Contact;
using AddressBook.Shared.Infrastructure.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddressBook.Shared.Contracts.Business
{
    public interface IContactService
    {
        Task<ICollection<ContactDto>> FindAllAsync();

        Task<ContactDto> FindByIdAsync(int id);

        Task CreateAsync(CreateContactDto dto);

        Task UpdateAsync(int contactId, UpdateContactDto dto);

        Task DeleteAsync(int contactId);

        Task<PagedResult<ContactPaginationDto>> GetPagedResultAsync(ContactPagingRequest contactPagingRequest);
    }
}
