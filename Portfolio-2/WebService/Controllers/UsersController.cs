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

        private object CreateResultUsers(int page, int pageSize, IList<User> users) 
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
        
        public UnauthorizedResult CheckCurrentUser()
        {
            if (Program.CurrentUser == null)
            {
                return Unauthorized();
            }
            return null;
        }
        
        // GET
        [HttpGet(Name = nameof(GetUsers))]
        public IActionResult GetUsers(int page = 0, int pageSize = 10)
        {
           pageSize = PaginationHelper.CheckPageSize(pageSize);
           var users = _dataService.GetUsers(page, pageSize);
           var result = CreateResultUsers(page, pageSize, users);
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
        
        [HttpGet("{uconst}/ratinghistory")]
        public IActionResult GetRatingHistory(string uconst)
        {
            CheckCurrentUser();
            var result = _dataService.GetRatingHistory(uconst);
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }
        
        [HttpGet("{uconst}/titlebookmarks/{tconst}")]
        public IActionResult GetTitleBookmark(string uconst, string tconst)
        {
            CheckCurrentUser();

            var titleBookmark = _dataService.GetTitleBookmark(uconst, tconst);
            if (titleBookmark == null)
            {
                return NoContent();
            }
            return Ok(titleBookmark);
        }


        [HttpGet("{uconst}/rating")]
        public IActionResult GetRatingsByUser(string uconst, int page = 0, int pageSize = 10)
        {
            CheckCurrentUser();
            var ratings = _dataService.GetRatingsByUser(uconst, page, pageSize);
            if (ratings == null)
            {
                return NoContent();
            }
            return Ok(ratings);
        }

        [HttpGet("{uconst}/rating/{tconst}")]
        public IActionResult GetRatingByUser(string uconst, string tconst)
        {
            CheckCurrentUser();
            var ratingByUser = _dataService.GetRatingByUser(uconst, tconst);
            if (ratingByUser == null)
            {
                return NoContent();
            }

            return Ok(ratingByUser);
        }

        [HttpGet("{uconst}/titlebookmarks")]
        public IActionResult GetTitleBookmarks(string uconst)
        {
            CheckCurrentUser();
            var titleBookmarks = _dataService.GetTitleBookmarks(uconst);
            if (titleBookmarks == null)
            {
                return NotFound();
            }
            return Ok(titleBookmarks);
        }
        
        [HttpGet("{uconst}/ratinghistory/{tconst}")]
        public IActionResult GetAllRatingHistory(string uconst, string tconst)
        {
            CheckCurrentUser();

            var result = _dataService.GetAllRatingHistory(uconst, tconst);
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpGet("{uconst}/searchhistory")]
        public IActionResult GetSearchHistory(string uconst)
        {
            CheckCurrentUser();

            var result = _dataService.GetSearchHistory(uconst);
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }
        
        
        // PUT
        
        
        // POST
        [HttpPost("{uconst}/titlebookmarks/{tconst}")]
        public IActionResult CreateTitleBookmark(string uconst, string tconst)
        {
            CheckCurrentUser();
            var titleBookmark = _dataService.CreateTitleBookmark(uconst, tconst);
            return Ok(titleBookmark);

        }
        
        // DELETE
        [HttpDelete("{uconst}/titlebookmarks/{tconst}")]
        public IActionResult DeleteTitleBookmark(string uconst, string tconst)
        {
            CheckCurrentUser();
            var titleBookmark = _dataService.DeleteTitleBookmark(uconst, tconst);
            if (titleBookmark)
            {
                return Ok();
            }
            return NotFound();
        }
        
    }
}
