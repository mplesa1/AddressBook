using AddressBook.DataAccessLayer.Persistence.Contexts;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.DataAccessLayer.Repositories
{
    public abstract class BaseRepository<T> where T : class
    {
        protected AppDbContext _dbContext;
        protected DbSet<T> _dbSet;
        protected readonly IUrlHelper _urlHelper;
        protected readonly IConfigurationProvider _configurationProvider;

        public BaseRepository(AppDbContext context, IUrlHelper urlHelper, IMapper mapper)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<T>();
            _urlHelper = urlHelper;
            _configurationProvider = mapper.ConfigurationProvider;
        }
    }
}
