﻿using AddressBook.Model;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.DataAccessLayer.Infrastructure
{
    public static class ModelBuilderExtensions
    {
        public static void SetRelations(this ModelBuilder builder)
        {
            builder.Entity<City>().HasMany(c => c.Settlements).WithOne(s => s.City).HasForeignKey(s => s.CityId);
            builder.Entity<Settlement>().HasMany(s => s.Contacts).WithOne(c => c.Settlement).HasForeignKey(c => c.SettlementId);
            builder.Entity<Contact>().HasMany(c => c.TelephoneNumbers).WithOne(tn => tn.Contact).HasForeignKey(tn => tn.ContactId);
        }

        public static void SetUniquesAndRequireds(this ModelBuilder builder)
        {
            builder.Entity<City>().HasIndex(c => c.Name).IsUnique();
            builder.Entity<Settlement>().HasIndex(s => s.Name).IsUnique();
            builder.Entity<Contact>().HasIndex(c => c.Name).IsUnique();
            builder.Entity<Contact>().HasIndex(c => c.Address).IsUnique();
            builder.Entity<TelephoneNumber>().HasIndex(tn => tn.Number).IsUnique();
        }

        public static void SetSeeds(this ModelBuilder builder)
        {
            builder.Entity<City>().HasData
            (
                new City { Id = 101, Name = "Zagreb" },
                new City { Id = 102, Name = "Velika Gorica" }
            );

            builder.Entity<Settlement>().HasData
            (
            new Settlement { Id = 201, Name = "Sloboština", PostalCode = "10010", TypeOfSettlement = ETypeOfSettlement.Neighborhood, CityId = 101 },
            new Settlement { Id = 202, Name = "Maksimir", PostalCode = "10000", TypeOfSettlement = ETypeOfSettlement.Neighborhood, CityId = 101 },
            new Settlement { Id = 203, Name = "Velika Gorica centar", PostalCode = "10408", TypeOfSettlement = ETypeOfSettlement.Neighborhood, CityId = 102 },
            new Settlement { Id = 204, Name = "Črnomerec", PostalCode = "10000", TypeOfSettlement = ETypeOfSettlement.Neighborhood, CityId = 101 }
            );
        }
    }
}
