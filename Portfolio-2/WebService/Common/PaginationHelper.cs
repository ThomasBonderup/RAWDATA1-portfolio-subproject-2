using System.Collections.Generic;
using DataAccess;

namespace WebService.Common
{
    public static class PaginationHelper
    {
        private const int MaxPageSize = 25;
        public static int CheckPageSize (int pageSize)
        {
            return pageSize > MaxPageSize ? MaxPageSize : pageSize;
        }
    }
}