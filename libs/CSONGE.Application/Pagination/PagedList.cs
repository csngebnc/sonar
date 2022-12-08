using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSONGE.Application.Pagination
{
    public class PagedList<T> : IPagedList<T>
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public long ItemCount { get; set; }

        public IList<T> Items { get; set; }

        public PagedList() => Items = Array.Empty<T>();

        public async static Task<PagedList<T>> ToPagedListAsync(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            var pagedList = new PagedList<T>();

            if (source is IQueryable<T> querable)
            {
                pagedList.PageIndex = pageIndex;
                pagedList.PageSize = pageSize;
                pagedList.ItemCount = querable.LongCount();

                pagedList.Items = await querable.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            else
            {
                pagedList.PageIndex = pageIndex;
                pagedList.PageSize = pageSize;
                pagedList.ItemCount = source.LongCount();

                pagedList.Items = await Task.FromResult(source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());
            }

            return pagedList;
        }
    }
}
