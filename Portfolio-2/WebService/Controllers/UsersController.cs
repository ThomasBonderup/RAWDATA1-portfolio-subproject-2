using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/users")]

    public class UsersController : ControllerBase
        {
        private IDataService _dataService;
        private IMapper _mapper;
        private int MaxPageSize = 25;
        
        public UsersController(IDataService dataService, IMapper mapper)
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
                prev = Url.Link(nameof(GetUsers), new {page = page - 1, pageSize});
            }

            string next = null;
            
            if (page < (int) Math.Ceiling((double)count / pageSize) - 1)
                next = Url.Link(nameof(GetUsers), new {page = page + 1, pageSize});

            var cur = Url.Link(nameof(GetUsers), new {page, pageSize});
            
            return (prev, cur, next);
        }

        private UserListDto CreateUserListDto(User user)
        {
            var dto = _mapper.Map<UserListDto>(user);
            dto.Url = Url.Link(nameof(GetUsers), new {user.Uconst});
            
            return dto;
        }

        private object CreateResult(int page, int pageSize, IList<User> users)
         {
             var items = users.Select(CreateUserListDto);

             var count = _dataService.NumberOfUsers();
                      
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

        [HttpGet(Name = nameof(GetUsers))]
        public IActionResult GetUsers(int page = 0, int pageSize = 10)
        {
            pageSize = CheckPageSize(pageSize);
            var users = _dataService.GetUsers(page, pageSize);

           var result = CreateResult(page, pageSize, users);
            return Ok(result);

        }
        [HttpGet("{uconst}")]

        public IActionResult GetUser(string uconst)
        {
            var user = _dataService.GetUser(uconst);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
}
}
