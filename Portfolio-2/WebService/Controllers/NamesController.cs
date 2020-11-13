using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Xunit.Abstractions;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/names")]
    
    public class NamesController : ControllerBase
    {
          
        private readonly ITestOutputHelper _testOutputHelper;

        public NamesController(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
        
        private IDataService _dataService;
        private IMapper _mapper;
        private int MaxPageSize = 25;
        
        public NamesController(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

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

        private NameListDto CreateNameListDto(Name name)
        {
            var dto = _mapper.Map<NameListDto>(name);
            dto.Url = Url.Link(nameof(GetNames), new {name.Nconst});
            
            return dto;
        }

        private object CreateResult(int page, int pageSize, IList<Name> names)
         {
             var items = names.Select(CreateNameListDto);

             var count = _dataService.NumberOfNames();
                      
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
        

        [HttpGet(Name = nameof(GetNames))]
        public IActionResult GetNames(int page = 0, int pageSize = 10)
        {
            pageSize = CheckPageSize(pageSize);
            var names = _dataService.GetNames(page, pageSize);

           var result = CreateResult(page, pageSize, names);
            return Ok(result);

        }
        [HttpGet("{nconst}")]

        public IActionResult GetName(string nconst)
        {
            var name = _dataService.GetName(nconst);
            if (name == null)
            {
                return NotFound();
            }
            return Ok(name);
        }
        

        [HttpGet("{nconst}")]

        public IActionResult GetNameRating(string nconst)
        {
            CheckCurrentUser();
            var nameRating = _dataService.GetNameRating(nconst);
            if (nameRating == null)
            {
                return NoContent();
            }
            return Ok(nameRating);
        }
        
        [HttpGet("{nconst}/primaryprofession")] //der kommer ingen data ud i postman?

        public IActionResult GetProfessions(string nconst){
            
            var professions = _dataService.GetProfessions(nconst);
            //Console.WriteLine(professions);
            //_testOutputHelper.WriteLine(professions.ToString());

            if (professions == null)
            {
                return NotFound();
            }
            return Ok(professions);
        }
    }

}
