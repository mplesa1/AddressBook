using AddressBook.Shared.DataTransferObjects.Settlement;
using AddressBook.Shared.DataTransferObjects.TelephoneNumber;
using System;
using System.Collections.Generic;

namespace AddressBook.Shared.DataTransferObjects.Contact
{
    public class ContactDto : BaseDto
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public DateTime DateOfBirth { get; set; }

        public SettlementDto Settlement { get; set; }

        public ICollection<TelephoneNumberDto> TelephoneNumbers { get; set; }
    }
}
