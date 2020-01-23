using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;

namespace AddressBook.Shared.Infrastructure.Pagination
{
    public abstract class AbstractPagingRequest<T>
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int? Page { get; set; }

        [Required]
        [Range(0, 200)]
        public int? PageSize { get; set; }

        public abstract IQueryable<T> GetFilteredQuery(IQueryable<T> query);

        public abstract IQueryable<T> SetUpSorting(IQueryable<T> query);

        private SortInformation GetSortInformation(string orderby)
        {
            SortInformation sortInformation;
            if (orderby.StartsWith("-"))
            {
                sortInformation = new SortInformation
                {
                    PropertyName = orderby.Substring(1, orderby.Length - 1),
                    SortDirection = ListSortDirection.Descending
                };
            }
            else
            {
                sortInformation = new SortInformation
                {
                    PropertyName = orderby,
                    SortDirection = ListSortDirection.Ascending
                };
            }

            return sortInformation;
        }

        /// <summary>
        /// Validates orderby string and constructs SortInformation object. If not valid returns null.
        /// </summary>
        /// <param name="orderBy"></param>
        /// <param name="validOrderByOptions"></param>
        /// <returns>If order is not valid null, otherwise insatnce of <see cref="SortInformation"/></returns>
        protected SortInformation ParseOrderBy(string orderBy, string validOrderByOptions)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
            {
                return null;
            }

            SortInformation sortInformation = GetSortInformation(orderBy);

            if (validOrderByOptions.Contains(sortInformation.PropertyName))
            {
                return sortInformation;
            }

            return null;
        }

        protected IQueryable<T> ApplyOrdering<TKey>(IQueryable<T> query, Expression<Func<T, TKey>> keySelector, ListSortDirection sortDirection)
        {
            if (sortDirection == ListSortDirection.Ascending)
            {
                query = query.OrderBy(keySelector);
            }
            else
            {
                query = query.OrderByDescending(keySelector);
            }

            return query;
        }
    }
}
