using System;
using System.Collections.Generic;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/names")]
    
   

    public class NamesController : ControllerBase
    {
        private IDataService _dataService;
        private int MaxPageSize = 25;

        
        private int CheckPageSize (int pageSize)
        {
            return pageSize > MaxPageSize ? MaxPageSize : pageSize;
        }

        private (string prev,string cur, string next) CreatePagingNavigation(int page, int pageSize, int count)
        {
            string prev = null;

            if (page > 0)
            {
                prev = Url.Link(nameof(GetNames), new {page = page - 1, pageSize});
            }

            string next = null;
            if (page < (int) Math.Ceiling((double)count / pageSize) - 1)
                next = Url.Link(nameof(GetNames), new {page = page + 1, pageSize});

            var cur = Url.Link(nameof(GetNames), new {page, pageSize});
            
            return (prev, cur, next);
        }

      /* private object CreateResult(int page, int pageSize, IList<Name> names)
        {
            var items 
                
                var count =_dataService.
                    
                    var navigationUrls = CreatePagingNavigation(page, pageSize, count);

                var result = new
                {
                    navigationUrls.prev,
                    navigationUrls.cur,
                    navigationUrls.next,
                    count,
                    items
                };
                
            return result;
        }
*/

        public NamesController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public IActionResult GetNames(int page = 0, int pageSize = 10)
        {
            pageSize = CheckPageSize(pageSize);
            var names = _dataService.GetNames(page, pageSize);

           //var result = CreateResult(page, pageSize, names);
            return Ok(names);

        }
}

}
