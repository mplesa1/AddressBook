using System.ComponentModel.DataAnnotations;

namespace AddressBook.Model
{
    public class TelephoneNumber : BaseEntity
    {
        public string Number { get; set; }

        [Required]
        public int ContactId { get; set; }
        public Contact Contact { get; set; }
    }
}
