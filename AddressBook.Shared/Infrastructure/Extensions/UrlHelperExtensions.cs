using AddressBook.Shared.Infrastructure.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using System;

namespace AddressBook.Shared.Infrastructure.Extensions
{
    public static class UrlHelperExtensions
    {
        private const string ACTION = "action";
        private const string CONTROLLER = "controller";

        public static string AbsolutePaginationUrlForPage<T>(
         this IUrlHelper urlHelper,
         AbstractPagingRequest<T> abstractPagingRequest,
         int page)
        {
            abstractPagingRequest.Page = page;
            return urlHelper.AbsoluteActionUrl(abstractPagingRequest);
        }

        public static string AbsoluteActionUrl<T>(this IUrlHelper urlHelper, AbstractPagingRequest<T> abstractPagingRequest)
        {
            if (urlHelper == null)
            {
                throw new ArgumentNullException(nameof(urlHelper));
            }

            HttpContext httpContext = urlHelper.ActionContext.HttpContext;
            RouteData routeData = httpContext.GetRouteData();
            string actionName = routeData.Values[ACTION] as string;
            string controllerName = routeData.Values[CONTROLLER] as string;
            UrlActionContext urlActionContext = new UrlActionContext();
            urlActionContext.Action = new UrlActionContext().Action = actionName;
            urlActionContext.Controller = new UrlActionContext().Controller = controllerName;
            urlActionContext.Values = abstractPagingRequest;
            urlActionContext.Protocol = urlHelper.ActionContext.HttpContext.Request.Scheme;
            return urlHelper.Action(urlActionContext);
        }
    }
}
