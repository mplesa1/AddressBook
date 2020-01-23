using AddressBook.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AddressBook.Shared.Infrastructure.Pagination
{
    public class ContactPagingRequest : AbstractPagingRequest<Contact>
    {
        private const string ValidOrderByValues = "name,address";

        public string Name { get; set; }

        public string Address { get; set; }

        public string OrderBy { get; set; }

        public override IQueryable<Contact> GetFilteredQuery(IQueryable<Contact> query)
        {
            if (!string.IsNullOrWhiteSpace(Name))
            {
                query = query.Where(s => EF.Functions.Like(s.Name, $"{Name}%"));
            }

            if (!string.IsNullOrWhiteSpace(Address))
            {
                query = query.Where(s => EF.Functions.Like(s.Address, $"{Address}%"));
            }

            return query;
        }

        public override IQueryable<Contact> SetUpSorting(IQueryable<Contact> query)
        {
            SortInformation sortInformation = ParseOrderBy(OrderBy, ValidOrderByValues);

            if (sortInformation == null)
            {
                return query;
            }

            switch (sortInformation.PropertyName)
            {
                case "name":
                    query = ApplyOrdering(query, s => s.Name, sortInformation.SortDirection);
                    break;
                case "address":
                    query = ApplyOrdering(query, s => s.Address, sortInformation.SortDirection);
                    break;
            }

            return query;
        }
    }
}
