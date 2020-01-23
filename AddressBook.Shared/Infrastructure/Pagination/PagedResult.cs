using System.Collections.Generic;

namespace AddressBook.Shared.Infrastructure.Pagination
{
    public class PagedResult<T>
    {
        public int Count { get; set; }

        public int PageCount { get; set; }

        public string NextPageUrl { get; set; }

        public string PreviousPageUrl { get; set; }

        public string FirstPagUrl { get; set; }

        public string LastPageUrl { get; set; }

        public IEnumerable<T> Data { get; set; }
    }
}
