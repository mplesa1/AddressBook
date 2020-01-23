using System.ComponentModel.DataAnnotations;

namespace AddressBook.Shared.DataTransferObjects.City
{
    public class CreateCityDto
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
