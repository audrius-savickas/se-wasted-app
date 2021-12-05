using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Helpers
{
    public static class ControllerBaseExtensions
    {
        public static void AddPaginationMetadata<T>(this ControllerBase controller, PagedList<T> pagedList)
        {
            var metadata = new
            {
                pagedList.TotalCount,
                pagedList.PageSize,
                pagedList.CurrentPage,
                pagedList.TotalPages,
                pagedList.HasNext,
                pagedList.HasPrevious,
            };

            controller.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
        }
    }
}
