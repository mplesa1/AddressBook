using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AddressBook.Model
{
    public class Settlement : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public string PostalCode { get; set; }

        public ETypeOfSettlement TypeOfSettlement { get; set; }

        [Required]
        public int CityId { get; set; }
        public City City { get; set; }

        public ICollection<Contact> Contacts { get; set; }
    }
}
