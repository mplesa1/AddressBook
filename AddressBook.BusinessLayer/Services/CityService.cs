﻿using AddressBook.Model;
using AddressBook.Shared.Contracts.Business;
using AddressBook.Shared.Contracts.DataAccess;
using AddressBook.Shared.DataTransferObjects.City;
using AddressBook.Shared.Infrastructure.Exceptions;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddressBook.BusinessLayer.Services
{
    public class CityService : BaseService, ICityService
    {
        private readonly ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository,
            IMapper mapper) : base(mapper)
        {
            _cityRepository = cityRepository;
        }

        public async Task<ICollection<CityDto>> FindAllAsync()
        {
            var cities = await _cityRepository.FindAllAsync();

            var cityDtos = Map<ICollection<City>, ICollection<CityDto>>(cities);

            return cityDtos;
        }

        public async Task CreateAsync(CreateCityDto dto)
        {
            var city = Map<CreateCityDto, City>(dto);
            await _cityRepository.CreateAsync(city);
        }

        public async Task UpdateAsync(int cityId, CreateCityDto dto)
        {
            var city = await _cityRepository.FindByIdAsync(cityId);

            if (city == null)
            {
                throw new ResourceNotFoundException();
            }

            MapToInstance(dto, city);

            await _cityRepository.UpdateAsync(city);
        }

        public async Task DeleteAsync(int cityId)
        {
            City city = await _cityRepository.FindByIdAsync(cityId);

            if (city == null)
            {
                throw new ResourceNotFoundException();
            }

            await _cityRepository.DeleteAsync(city);
        }
    }
}
