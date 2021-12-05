using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Extensions
{
    public static class PagedListExtensions
    {
        public static PagedList<TOutput> ConvertAllItems<T,TOutput>(this PagedList<T> source, Converter<T,TOutput> converter)
        {
            var convertedList = source.Select(x => converter(x)).ToList();
            return new PagedList<TOutput>(convertedList, source.Count, source.CurrentPage, source.PageSize);
        }
    }
}
