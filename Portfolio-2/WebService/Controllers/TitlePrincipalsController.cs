using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using WebService.Common;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/titleprincipals")]
    public class TitlePrincipalsController : ControllerBase
    {
        /*private readonly IDataService _dataService;
        private readonly IMapper _mapper;
        private int MaxPageSize = 25;
        
        public TitlePrincipalsController(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }
        
        private (string prev,string cur, string next) CreatePagingNavigation(int page, int pageSize, int count)
        {
            string prev = null;

            if (page > 0)
            {
                prev = Url.Link(nameof(GetTitlesPrincipalsListList), new {page = page - 1, pageSize});
            }

            string next = null;
            
            if (page < (int) Math.Ceiling((double)count / pageSize) - 1)
                next = Url.Link(nameof(GetTitlesPrincipalsListList), new {page = page + 1, pageSize});

            var cur = Url.Link(nameof(GetTitlesPrincipalsListList), new {page, pageSize});
            
            return (prev, cur, next);
        }
        
        private TitlePrincipalsDto CreateTitlePrincipalsListDto(TitlePrincipals titlePrincipals)
        {
            var dto = _mapper.Map<TitlePrincipalsDto>(titlePrincipals);
            dto.Url = Url.Link(nameof(TitlesController.GetTitlePrincipals), new {titlePrincipals.Tconst});

            return dto;
        }
        
        private object CreateResultTitlePrincipals(int page, int pageSize, IList<TitlePrincipals> titlePrincipals)
        {
            var items = titlePrincipals.Select(CreateTitlePrincipalsListDto);
            var count = _dataService.NumberOfTitlePrincipals();
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
        
        public UnauthorizedResult CheckCurrentUser()
        {
            if (Program.CurrentUser == null)
            {
                return Unauthorized();
            }
            return null;
        }
        
        
        [HttpGet(Name = nameof(GetTitlesPrincipalsListList))]
        public IActionResult GetTitlesPrincipalsListList(int page = 0, int pageSize = 10)
        {
            CheckCurrentUser();
            pageSize = PaginationHelper.CheckPageSize(pageSize);
            var titlePrincipals = _dataService.GetTitlePrincipalsList(page, pageSize);
            var result = CreateResultTitlePrincipals(page, pageSize, titlePrincipals);
            return Ok(result);
        }
        
        
        [HttpGet("{searchString}")]
        public IActionResult SearchTitlePrincipals(string searchString, string uConst, int page = 0, int pageSize = 10)
        {
            CheckCurrentUser();

            pageSize = PaginationHelper.CheckPageSize(pageSize);
            var titles = _dataService.SearchTitlePrincipals(searchString, Program.CurrentUser.Uconst, page, pageSize);

            if (titles == null)
            {
                return NotFound();
            }

            var result = CreateResultTitlePrincipals(page, pageSize, titles);
            
            return Ok(result);
        }*/
    }
}