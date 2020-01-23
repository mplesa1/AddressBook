using AddressBook.Model;
using AddressBook.Model.Extensions;
using AddressBook.Shared.DataTransferObjects.City;
using AddressBook.Shared.DataTransferObjects.Contact;
using AddressBook.Shared.DataTransferObjects.Settlement;
using AddressBook.Shared.DataTransferObjects.TelephoneNumber;
using AutoMapper;

namespace AddressBook.BusinessLayer.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<City, CityDto>();
            CreateMap<CreateCityDto, City>();

            CreateMap<Settlement, SettlementDto>()
                .ForMember(src => src.TypeOfSettlement,
                           opt => opt.MapFrom(src => src.TypeOfSettlement.ToDescriptionString()));
            CreateMap<CreateSettlementDto, Settlement>()
                .ForMember(src => src.TypeOfSettlement, opt => opt.MapFrom(src => (ETypeOfSettlement)src.TypeOfSettlement));

            CreateMap<Contact, ContactDto>();
            CreateMap<CreateContactDto, Contact>();
            CreateMap<UpdateContactDto, Contact>();
            CreateMap<Contact, ContactPaginationDto>();

            CreateMap<TelephoneNumber, TelephoneNumberDto>();
            CreateMap<CreateTelephoneNumberDto, TelephoneNumber>();
            CreateMap<UpdateTelephoneNumberDto, TelephoneNumber>();
        }
    }
}
