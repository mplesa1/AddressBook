using AddressBook.DataAccessLayer.Infrastructure;
using AddressBook.Model;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.DataAccessLayer.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Settlement> Settlements { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<TelephoneNumber> TelephoneNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.SetRelations();
            builder.SetUniquesAndRequireds();
            builder.SetSeeds();
        }
    }
}
