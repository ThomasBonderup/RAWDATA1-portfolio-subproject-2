using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Xml;
using AutoMapper;
//using AutoMapper.Configuration;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop.Infrastructure;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using WebService.Common;
using WebService.Models;
using WebService.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private IDataService _dataService;
        private IMapper _mapper;
        private int MaxPageSize = 25;
        private IConfiguration _configuration;


        public UsersController(IDataService dataService, IMapper mapper, IConfiguration configuration)
        {
            _dataService = dataService;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            //if (_dataService.GetUser(dto.UserName) != null)
            //{
            //    return BadRequest();
            //} 
            int.TryParse(_configuration.GetSection("Auth:PasswordSize").Value, out int pwdSize);
            if (pwdSize == 0)
            {
                throw new ArgumentException("No Password Size");
            }

            var salt = PasswordService.GenerateSalt(pwdSize);
            var pwd = PasswordService.HashPassword(dto.Password, salt, pwdSize);

            _dataService.CreateUser(dto.FirstName, dto.LastName, dto.Email, dto.UserName, pwd, salt);

            return CreatedAtRoute(null, new {dto.UserName});
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var user = _dataService.GetUser(dto.Username);
            if (user == null)
            {
                return BadRequest();
            }

            int.TryParse(_configuration.GetSection("Auth:PasswordSize").Value, out int pwdSize);

            if (pwdSize == 0)
            {
                throw new ArgumentException("No password size");
            }

            string secret = _configuration.GetSection("Auth:secret").Value;
            if (string.IsNullOrEmpty(secret))
            {
                throw new Exception("No secret");
            }

            var password = PasswordService.HashPassword(dto.Password, user.Salt, pwdSize);

            if (password != user.Password)
            {
                return BadRequest();
            }
            
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(secret);
            
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]{new Claim("uconst",user.Uconst) }),
                Expires = DateTime.Now.AddSeconds(450), //skal nok sættes højere
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature )
            };

            var securityToken = tokenHandler.CreateToken(tokenDescription);
            var token = tokenHandler.WriteToken(securityToken);

            return Ok(new {dto.Username, token});
        }

        private (string prev, string cur, string next) CreatePagingNavigation(int page, int pageSize, int count)
        {
            string prev = null;

            if (page > 0)
            {
                prev = Url.Link(nameof(GetUsers), new {page = page - 1, pageSize});
            }

            string next = null;

            if (page < (int) Math.Ceiling((double) count / pageSize) - 1)
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

        // FROM UCONST TO USERNAME
        [HttpGet("{username}")]
        public IActionResult GetUser(string username)
        {
            var user = _dataService.GetUser(username);
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

        [HttpGet("{uconst}/titlenotes/{tconst}")]
        public IActionResult GetTitleNote(string uconst, string tconst)
        {
            CheckCurrentUser();

            var titleNote = _dataService.GetTitleNote(uconst, tconst);
            if (titleNote == null)
            {
                return NoContent();
            }

            return Ok(titleNote);
        }

        [HttpGet("{uconst}/titlenotes")]
        public IActionResult GetTitleNotes(string uconst)
        {
            CheckCurrentUser();
            var titleNotes = _dataService.GetTitleNotes(uconst);
            if (titleNotes == null)
            {
                return NotFound();
            }

            return Ok(titleNotes);
        }

        [HttpGet("{uconst}/namebookmarks/{nconst}")]
        public IActionResult GetNameBookmark(string uconst, string nconst)
        {
            CheckCurrentUser();

            var nameBookmark = _dataService.GetNameBookmark(uconst, nconst);
            if (nameBookmark == null)
            {
                return NoContent();
            }

            return Ok(nameBookmark);
        }

        [HttpGet("{uconst}/namebookmarks")]
        public IActionResult GetNameBookmarks(string uconst)
        {
            CheckCurrentUser();
            var nameBookmarks = _dataService.GetNameBookmarks(uconst);
            if (nameBookmarks == null)
            {
                return NotFound();
            }

            return Ok(nameBookmarks);
        }

        [HttpGet("{uconst}/namenotes/{nconst}")]
        public IActionResult GetNameNote(string uconst, string nconst)
        {
            CheckCurrentUser();

            var nameNote = _dataService.GetNameNote(uconst, nconst);
            if (nameNote == null)
            {
                return NoContent();
            }

            return Ok(nameNote);
        }

        [HttpGet("{uconst}/namenotes")]
        public IActionResult GetNameNotes(string uconst)
        {
            CheckCurrentUser();
            var nameNotes = _dataService.GetNameNotes(uconst);
            if (nameNotes == null)
            {
                return NotFound();
            }

            return Ok(nameNotes);
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

        [HttpPut("{uconst}")]
        public IActionResult UpdateUser(User user)
        {
            CheckCurrentUser();
            var result = _dataService.GetUser(user.Uconst);
            if (result != null)
            {
                _dataService.UpdateUser(user.Uconst, user.FirstName, user.LastName, user.Email, user.Password,
                    user.UserName);

                return Ok(user);
            }

            return NotFound();
        }

        [HttpPut("{uconst}/namenotes/{nconst}")]
        public IActionResult UpdateNameNote(NameNotes nameNotes)
        {
            CheckCurrentUser();
            var result = _dataService.GetNameNote(nameNotes.Uconst, nameNotes.Nconst);
            if (result != null)
            {
                _dataService.UpdateNameNote(nameNotes.Uconst, nameNotes.Nconst, nameNotes.Notes);

                return Ok(nameNotes);
            }

            return NotFound();
        }

        [HttpPut("{uconst}/titlenotes/{tconst}")]
        public IActionResult UpdateTitleNote(TitleNotes titleNotes)
        {
            CheckCurrentUser();
            var result = _dataService.GetTitleNote(titleNotes.Uconst, titleNotes.Tconst);
            if (result != null)
            {
                _dataService.UpdateTitleNote(titleNotes.Uconst, titleNotes.Tconst, titleNotes.Notes);

                return Ok(titleNotes);
            }

            return NotFound();
        }


        // POST
        [HttpPost("{uconst}/titlebookmarks/{tconst}")]
        public IActionResult CreateTitleBookmark(string uconst, string tconst)
        {
            CheckCurrentUser();
            var titleBookmark = _dataService.CreateTitleBookmark(uconst, tconst);
            return Ok(titleBookmark);
        }

        [HttpPost("{uconst}/titlenotes/{tconst}")]
        public IActionResult CreateTitleNote(TitleNotes titleNote)
        {
            CheckCurrentUser();
            var result = _dataService.CreateTitleNote(titleNote.Uconst, titleNote.Tconst, titleNote.Notes);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            CheckCurrentUser();
            var result =
                _dataService.CreateUser(user.FirstName, user.LastName, user.Email, user.Password, user.UserName);
            return Ok(result);
        }

        [HttpPost("{uconst}/namenotes/{nconst}")]
        public IActionResult CreateNameNote(NameNotes nameNote)
        {
            CheckCurrentUser();
            var result = _dataService.CreateNameNote(nameNote.Uconst, nameNote.Nconst, nameNote.Notes);

            return Ok(result);
        }

        [HttpPost("{uconst}/namebookmarks/{nconst}")]
        public IActionResult CreateNameBookmark(NameBookmark nameBookmark)
        {
            CheckCurrentUser();
            var result = _dataService.CreateNameBookmark(nameBookmark.Uconst, nameBookmark.Nconst);
            return Ok(result);
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

        [HttpDelete("{uconst}")]
        public IActionResult DeleteUser(string uconst)
        {
            CheckCurrentUser();
            var user = _dataService.DeleteUser(uconst);
            if (!user)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("{uconst}/namenotes/{nconst}")]
        public IActionResult DeleteNameNote(string uconst, string nconst)
        {
            CheckCurrentUser();
            var nameNote = _dataService.DeleteNameNote(uconst, nconst);
            if (nameNote)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpDelete("{uconst}/titlenotes/{tconst}")]
        public IActionResult DeleteTitleNote(string uconst, string tconst)
        {
            CheckCurrentUser();
            var titleNote = _dataService.DeleteTitleNote(uconst, tconst);
            if (titleNote)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpDelete("{uconst}/namebookmarks/{nconst}")]
        public IActionResult DeleteNameBookmark(string uconst, string nconst)
        {
            CheckCurrentUser();
            var nameBookMark = _dataService.DeleteNameBookmark(uconst, nconst);
            if (nameBookMark)
            {
                return Ok();
            }

            return NotFound();
        }
    }
}