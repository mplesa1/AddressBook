using System.ComponentModel.DataAnnotations;

namespace AddressBook.Shared.DataTransferObjects.TelephoneNumber
{
    public class CreateTelephoneNumberDto
    {
        [Required]
        [MaxLength(20)]
        public string Number { get; set; }
    }
}
