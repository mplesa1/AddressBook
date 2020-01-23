using System;

namespace AddressBook.Shared.DataTransferObjects.Contact
{
    public class ContactPaginationDto : BaseDto
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
