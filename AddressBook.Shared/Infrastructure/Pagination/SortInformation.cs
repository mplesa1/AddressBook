using System.ComponentModel;

namespace AddressBook.Shared.Infrastructure.Pagination
{
    public class SortInformation
    {
        public string PropertyName { get; set; }

        public ListSortDirection SortDirection { get; set; }
    }
}
