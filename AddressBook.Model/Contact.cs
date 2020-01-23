using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AddressBook.Model
{
    public class Contact : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Required]
        public int SettlementId { get; set; }
        public Settlement Settlement { get; set; }

        public ICollection<TelephoneNumber> TelephoneNumbers { get; set; }
    }
}
