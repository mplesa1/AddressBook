using AddressBook.Model;
using AddressBook.Shared.Contracts.Business;
using AddressBook.Shared.Contracts.DataAccess;
using AddressBook.Shared.DataTransferObjects.Contact;
using AddressBook.Shared.Infrastructure.Exceptions;
using AddressBook.Shared.Infrastructure.Pagination;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBook.BusinessLayer.Services
{
    public class ContactService : BaseService, IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository, IMapper mapper) : base(mapper)
        {
            _contactRepository = contactRepository;
        }

        public async Task CreateAsync(CreateContactDto dto)
        {
            Contact existingContact = await _contactRepository.FindByNameOrAddressAsync(dto.Name, dto.Address);
            if (existingContact != null)
            {
                if (existingContact.Name.ToUpper() == dto.Name.ToUpper())
                {
                    throw new BusinessException("Contact already exist with name " + dto.Name);
                }

                if (existingContact.Address.ToUpper() == dto.Address.ToUpper())
                {
                    throw new BusinessException("Contact already exist with address " + dto.Address);
                }
            }

            var contact = Map<CreateContactDto, Contact>(dto);
            await _contactRepository.CreateAsync(contact);
        }

        public async Task DeleteAsync(int contactId)
        {
            Contact contact = await _contactRepository.FindByIdAsync(contactId);

            if (contact == null)
            {
                throw new ResourceNotFoundException();
            }

            await _contactRepository.DeleteAsync(contact);
        }

        public async Task<ICollection<ContactDto>> FindAllAsync()
        {
            var contacts = await _contactRepository.FindAllAsync();

            var contactDtos = Map<ICollection<Contact>, ICollection<ContactDto>>(contacts);

            return contactDtos;
        }

        public async Task<ContactDto> FindByIdAsync(int id)
        {
            var contact = await _contactRepository.FindByIdAsync(id);
            var contactDto = Map<Contact, ContactDto>(contact);

            return contactDto;
        }

        public async Task UpdateAsync(int contactId, UpdateContactDto  dto)
        {
            var contact = await _contactRepository.FindByIdAsync(contactId);

            if (contact == null)
            {
                throw new ResourceNotFoundException();
            }

            var isExistByNameOrAddress = await _contactRepository.IsExistByNameOrAddressAndContactIdAsync(dto.Name, dto.Address, contactId);

            if (isExistByNameOrAddress)
            {
                throw new BusinessException("Contact already exist with name or address");
            }

            MapToInstance(dto, contact);

            foreach (var number in dto.TelephoneNumbers)
            {
                var exsisintNumber = contact.TelephoneNumbers.FirstOrDefault(tn => tn.Id == number.Id);
                exsisintNumber.Number = number.Number;
            }

            await _contactRepository.UpdateAsync(contact);
        }

        public async Task<PagedResult<ContactPaginationDto>> GetPagedResultAsync(ContactPagingRequest contactPagingRequest)
        {
            PagedResult<ContactPaginationDto> pagedResult = await _contactRepository.GetPagedResultAsync(contactPagingRequest);
            return pagedResult;
        }
    }
}
