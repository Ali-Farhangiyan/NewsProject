
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pagination
{
    public class PaginatedList<T> : List<T>
    {
        public int TotalPage { get; set; }

        public int PageIndex { get; set; }

        public bool HasNextPage => PageIndex < TotalPage;
        public bool HasPrevPage => PageIndex > 1;

        public PaginatedList(List<T> items, int Count, int PageSize, int PageIndex)
        {
            this.PageIndex = PageIndex;
            TotalPage = (int)Math.Ceiling(Count / (double)PageSize);

            this.AddRange(items);
        }

        public static PaginatedList<T> Create(List<T> model, int pageSize, int pageIndex)
        {
            int count =  model.Count();
            var takeModel =  model.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(takeModel, count, pageSize, pageIndex);
        }
    }
}
