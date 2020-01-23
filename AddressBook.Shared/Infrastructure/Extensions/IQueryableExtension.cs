using AddressBook.Shared.Infrastructure.Pagination;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBook.Shared.Infrastructure.Extensions
{
    public static class IQueryableExtension
    {
        public static async Task<PagedResult<TDto>> ToPagedResultAsync<TEntity, TDto>(
           this IQueryable<TEntity> query,
           AbstractPagingRequest<TEntity> pagingRequest,
           IConfigurationProvider configurationProvider,
           IUrlHelper urlHelper)
        {
            query = query.ApplyFilter(pagingRequest);

            int totalResults = await query.CountAsync();
            IEnumerable<TDto> data = await query.ApplySorting(pagingRequest)
                .ApplyPagination(pagingRequest)
                .ProjectTo<TDto>(configurationProvider)
                .ToListAsync();
            PagedResult<TDto> pagedResult = new PagedResult<TDto>
            {
                Count = totalResults,
                Data = data
            };
            if (pagingRequest.PageSize != 0)
            {
                int pageCount = (int)Math.Ceiling(totalResults / (double)pagingRequest.PageSize);
                pagedResult.PageCount = pageCount;
                int requestedPage = pagingRequest.Page.Value;

                if (requestedPage < pageCount)
                {
                    pagedResult.NextPageUrl = urlHelper.AbsolutePaginationUrlForPage(pagingRequest, requestedPage + 1);
                }

                if (requestedPage > 1)
                {
                    pagedResult.PreviousPageUrl = urlHelper.AbsolutePaginationUrlForPage(pagingRequest, requestedPage - 1);
                }

                pagedResult.FirstPagUrl = urlHelper.AbsolutePaginationUrlForPage(pagingRequest, 1);
                pagedResult.LastPageUrl = urlHelper.AbsolutePaginationUrlForPage(pagingRequest, pageCount);
            }

            return pagedResult;
        }

        public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> query, AbstractPagingRequest<T> pagingRequest)
        {
            return pagingRequest.GetFilteredQuery(query);
        }

        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, AbstractPagingRequest<T> pagingRequest)
        {
            if (pagingRequest.PageSize.Value == 0)
            {
                return query;
            }
            int numberOfElementsToSkip = (pagingRequest.Page.Value - 1) * pagingRequest.PageSize.Value;
            return query.Skip(numberOfElementsToSkip).Take(pagingRequest.PageSize.Value);
        }

        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, AbstractPagingRequest<T> pagingRequest)
        {
            return pagingRequest.SetUpSorting(query);
        }
    }
}
