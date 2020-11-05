using AutoMapper;
using DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/titles")]
    public class TitlesController : ControllerBase
    {
        IDataService _dataService;
        //private readonly IMapper _mapper;

        public TitlesController(IDataService dataService)
        {
            _dataService = dataService;
            //_mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetTitles()
        {
            var titles = _dataService.GetTitles();

            return Ok(titles);
        }

        [HttpGet("{tconst}")]

        public IActionResult GetTitle(string tconst)
        {
            var title = _dataService.GetTitle(tconst);
            if (title == null)
            {
                return NotFound();
            }
            return Ok(title);
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

    }
}