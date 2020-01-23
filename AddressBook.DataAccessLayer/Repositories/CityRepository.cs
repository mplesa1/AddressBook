using AddressBook.DataAccessLayer.Persistence.Contexts;
using AddressBook.Model;
using AddressBook.Shared.Contracts.DataAccess;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddressBook.DataAccessLayer.Repositories
{
    public class CityRepository : BaseRepository<City>, ICityRepository
    {
        public CityRepository(AppDbContext context, IUrlHelper urlHelper, IMapper mapper) : base(context, urlHelper, mapper)
        {
        }

        public async Task CreateAsync(City city)
        {
            await _dbSet.AddAsync(city);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<City>> FindAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task UpdateAsync(City city)
        {
            _dbSet.Update(city);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<City> FindByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task DeleteAsync(City city)
        {
            _dbSet.Remove(city);
            await _dbContext.SaveChangesAsync();
        }
    }
}
