using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class PagedResultSet<TEntity> where TEntity : class
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRowCount { get; set; }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
        public IEnumerable<TEntity> Data { get; set; }

        public PagedResultSet(IEnumerable<TEntity> data, int pageIndex, int pageSize, int totalRowCount)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalRowCount = totalRowCount;
            Data = data;
            TotalPages = (int)Math.Ceiling(TotalRowCount / (double)pageSize);
         }
    }
}
