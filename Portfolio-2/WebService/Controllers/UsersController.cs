using AutoMapper;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;


namespace WebService.Controllers
{
    [ApiController]
    [Route("api/users")]

    public class UsersController : ControllerBase
    {
        private IDataService _dataService;
        private IMapper _mapper;


        [HttpGet("uconst")]

        public IActionResult GetUsers(string uconst)
        {
            var users = _dataService.GetUser(uconst);
            if (users == null)
            {
                return NotFound();
            }
            return Ok(User);
        }

    }
}
