using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI.Data.Dtos;

namespace TestAPI.Helpers
{
    public class PaginatedList<T> : List<T>
    {

        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages { get; set; }

        public PaginateDto paginateDto { get; set; }

        public PaginatedList(List<T> source, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = source.Count();
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);

            paginateDto = getPaginate(PageIndex, PageSize, TotalPages);
            this.AddRange(source.Skip((PageIndex - 1) * PageSize).Take(PageSize));
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 0);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex + 1 < TotalPages);
            }
        }

        public PaginateDto getPaginate(int pageIndex, int pageSize, int totalPages){
            var paginate = new PaginateDto();
          
            paginate.currentPage = pageIndex;
            paginate.sizePages = pageSize;
            paginate.totalPages = totalPages;

            return paginate;
        }
    }
}