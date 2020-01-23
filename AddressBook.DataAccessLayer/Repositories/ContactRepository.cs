using AddressBook.DataAccessLayer.Persistence.Contexts;
using AddressBook.Model;
using AddressBook.Shared.Contracts.DataAccess;
using AddressBook.Shared.DataTransferObjects.Contact;
using AddressBook.Shared.Infrastructure.Extensions;
using AddressBook.Shared.Infrastructure.Pagination;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddressBook.DataAccessLayer.Repositories
{
    public class ContactRepository : BaseRepository<Contact>, IContactRepository
    {
        public ContactRepository(AppDbContext context, IUrlHelper urlHelper, IMapper mapper) : base(context, urlHelper, mapper)
        {
        }

        public async Task CreateAsync(Contact contact)
        {
            await _dbSet.AddAsync(contact);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Contact contact)
        {
            _dbSet.Remove(contact);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<Contact>> FindAllAsync()
        {
            var contacts = await _dbSet.Include(c => c.Settlement)
                                       .Include(c => c.TelephoneNumbers)
                                       .ToListAsync();
            return contacts;
        }

        public async Task<Contact> FindByIdAsync(int id)
        {
            var contact = await _dbSet.Include(c => c.Settlement)
                                      .Include(c => c.TelephoneNumbers)
                                      .FirstOrDefaultAsync(c => c.Id == id);
            return contact;
        }

        public async Task UpdateAsync(Contact contact)
        {
            _dbSet.Update(contact);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<PagedResult<ContactPaginationDto>> GetPagedResultAsync(ContactPagingRequest contactPagingRequest)
        {
            PagedResult<ContactPaginationDto> pagedResult = await _dbSet.AsQueryable().ToPagedResultAsync<Contact, ContactPaginationDto>(contactPagingRequest, _configurationProvider, _urlHelper);
            return pagedResult;
        }

        public async Task<Contact> FindByNameOrAddressAsync(string name, string address)
        {
            var contact = await _dbSet.FirstOrDefaultAsync(c => c.Name.ToUpper() == name.ToUpper() || 
                                                           c.Address.ToUpper() == address.ToUpper());
            return contact;
        }

        public async Task<bool> IsExistByNameOrAddressAndContactIdAsync(string name, string address, int contactId)
        {
            var isExist = await _dbSet.AnyAsync(c => (c.Name.ToUpper() == name.ToUpper() || c.Address.ToUpper() == address.ToUpper()) 
                                                && c.Id != contactId);

            return isExist;
        }
    }
}
