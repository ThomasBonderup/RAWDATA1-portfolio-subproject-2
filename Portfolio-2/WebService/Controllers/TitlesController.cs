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
    [Route("api/titles")]
    public class TitlesController : ControllerBase
    {
        IDataService _dataService;
        private readonly IMapper _mapper;

        public TitlesController(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }
        
        private (string prev,string cur, string next) CreatePagingNavigation(int page, int pageSize, int count)
        {
            string prev = null;

            if (page > 0)
            {
                prev = Url.Link(nameof(GetTitles), new {page = page - 1, pageSize});
            }

            string next = null;
            
            if (page < (int) Math.Ceiling((double)count / pageSize) - 1)
                next = Url.Link(nameof(GetTitles), new {page = page + 1, pageSize});

            var cur = Url.Link(nameof(GetTitles), new {page, pageSize});
            
            return (prev, cur, next);
        }
        

        private TitleListDto CreateTitleListDto(Title title)
        {
            var dto = _mapper.Map<TitleListDto>(title);
            dto.Url = Url.Link(nameof(GetTitles), new {title.Tconst});

            return dto;
        }
        

        private object CreateResultTitles(int page, int pageSize, IList<Title> titles)
        {
            var items = titles.Select(CreateTitleListDto);
            var count = _dataService.NumberOfTitles();
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
        
        
        // GET
        [HttpGet(Name = nameof(GetTitles))]
        public IActionResult GetTitles(int page = 0, int pageSize = 10)
        {
            CheckCurrentUser();
            
            pageSize = PaginationHelper.CheckPageSize(pageSize);
            var titles = _dataService.GetTitles(page, pageSize);

            var result = CreateResultTitles(page, pageSize, titles);
            
            return Ok(result);
        }
        
        [HttpGet("{tconst}")]
        public IActionResult GetTitle(string tconst)
        {
            
            CheckCurrentUser();
            
            var title = _dataService.GetTitle(tconst);
            if (title == null)
            {
                return NotFound();
            }
            return Ok(title);
        }

        [HttpGet("{tconst}, {nconst}")]

        public IActionResult GetTitlePrincipals(string tconst, string nconst)
        {
            CheckCurrentUser();

            var titlePrincipals = _dataService.GetTitlePrincipals(tconst, nconst);
            if (titlePrincipals == null)
            {
                return NotFound();
            }
            return Ok(titlePrincipals);
        }


        // POST
        
        [HttpPost("{tconst}")]
        public IActionResult CreateTitle(string titleType, string primaryTitle, string originalTitle, bool isAdult, 
            string startYear, string endYear, int runtimeMinutes, string poster, string awards, string plot)
        {
            CheckCurrentUser();
           // var title = _dataService.CreateTitle(primaryTitle);
           var title = _dataService.CreateTitle(titleType, primaryTitle, originalTitle, isAdult, startYear, endYear,
               runtimeMinutes, poster,
               awards, plot);
            return Ok(title);
        }

        // PUT

        [HttpPut("{tconst}")]

        public IActionResult UpdateTitle(string tconst, string titleType, string primaryTitle, string originalTitle, bool isAdult, 
            string startYear, string endYear, int runtimeMinutes, string poster, string awards, string plot)
        {

            CheckCurrentUser();
            
            var title = _dataService.GetTitle(tconst);

            if (title != null)
            {
                _dataService.UpdateTitle(tconst, titleType, primaryTitle, originalTitle, isAdult, startYear, endYear,
                    runtimeMinutes, poster, awards, plot);
                return Ok(title);
            }

            return NotFound();
        }


        // DELETE
        
        
    

    }
}