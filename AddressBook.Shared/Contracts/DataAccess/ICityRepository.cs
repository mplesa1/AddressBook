﻿using AddressBook.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddressBook.Shared.Contracts.DataAccess
{
    public interface ICityRepository
    {
        Task<ICollection<City>> FindAllAsync();

        Task CreateAsync(City city);

        Task UpdateAsync(City city);

        Task<City> FindByIdAsync(int id);

        Task DeleteAsync(City city);
    }
}
