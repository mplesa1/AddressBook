using AddressBook.Api.Extensions;
using AddressBook.Shared.Contracts.Business;
using AddressBook.Shared.DataTransferObjects.Contact;
using AddressBook.Shared.Infrastructure.Pagination;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AddressBook.Api.Controllers
{
    public class ContactController : BaseController
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var contacts = await _contactService.FindAllAsync();
            return ApiResponseOk(contacts);
        }

        [HttpGet("{contactId}")]
        public async Task<IActionResult> GetByIdAsync(int contactId)
        {
            var contact = await _contactService.FindByIdAsync(contactId);
            return ApiResponseOk(contact);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateContactDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            await _contactService.CreateAsync(request);
            return ApiResponseOk();
        }

        [HttpPut("{contactId}")]
        public async Task<IActionResult> UpdateCity(int contactId, [FromBody]UpdateContactDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            await _contactService.UpdateAsync(contactId, request);
            return ApiResponseOk(null);
        }

        [HttpDelete("{contactId}")]
        public async Task<IActionResult> DeleteCity(int contactId)
        {
            await _contactService.DeleteAsync(contactId);
            return ApiResponseOk();
        }

        [HttpGet("pagination")]
        [ProducesResponseType(typeof(PagedResult<ContactPaginationDto>), 200)]
        public async Task<IActionResult> GetPage([FromQuery] ContactPagingRequest contactPagingRequest)
        {
            PagedResult<ContactPaginationDto> pagedResult = await _contactService.GetPagedResultAsync(contactPagingRequest);
            return ApiResponseOk(pagedResult);
        }
    }
}