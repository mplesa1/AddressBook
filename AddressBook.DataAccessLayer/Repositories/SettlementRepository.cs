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
    public class SettlementRepository : BaseRepository<Settlement>, ISettlementRepository
    {
        public SettlementRepository(AppDbContext context, IUrlHelper urlHelper, IMapper mapper) : base(context, urlHelper, mapper)
        {
        }

        public async Task CreateAsync(Settlement settlement)
        {
            await _dbSet.AddAsync(settlement);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<Settlement>> FindAllAsync()
        {
            return await _dbSet
                .Include(s => s.City)
                .ToListAsync();
        }

        public async Task UpdateAsync(Settlement settlement)
        {
            _dbSet.Update(settlement);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Settlement> FindByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task DeleteAsync(Settlement settlement)
        {
            _dbSet.Remove(settlement);
            await _dbContext.SaveChangesAsync();
        }
    }
}