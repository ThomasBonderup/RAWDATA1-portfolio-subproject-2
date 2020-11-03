using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/names")]
    public class NamesController : ControllerBase
    {
        private IDataService _dataService;

        public NamesController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public IActionResult GetNames()
        {
            var names = _dataService.GetNames();
            return Ok(names);

        }
}

}
