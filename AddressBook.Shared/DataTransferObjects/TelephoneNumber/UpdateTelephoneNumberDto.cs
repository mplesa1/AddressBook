using System.ComponentModel.DataAnnotations;

namespace AddressBook.Shared.DataTransferObjects.TelephoneNumber
{
    public class UpdateTelephoneNumberDto : BaseDto
    {
        [Required]
        [MaxLength(20)]
        public string Number { get; set; }
    }
}
