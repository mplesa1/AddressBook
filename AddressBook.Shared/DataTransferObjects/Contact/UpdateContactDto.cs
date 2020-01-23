using AddressBook.Shared.DataTransferObjects.TelephoneNumber;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AddressBook.Shared.DataTransferObjects.Contact
{
    public class UpdateContactDto
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        public string Address { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Required]
        public int SettlementId { get; set; }

        [Required]
        public ICollection<UpdateTelephoneNumberDto> TelephoneNumbers { get; set; }
    }
}
