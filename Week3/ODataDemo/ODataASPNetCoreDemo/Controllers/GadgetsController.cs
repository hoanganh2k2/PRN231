using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using ODataASPNetCoreDemo.Data.Entities;

namespace ODataASPNetCoreDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GadgetsController : ControllerBase
    {
        private readonly MyDbContext _db;

        public GadgetsController(MyDbContext db)
        {
            _db = db;
        }

        [EnableQuery]
        [HttpGet("Get")]
        public IActionResult Get()
        {
            return Ok(_db.Gadgets.AsQueryable());
        }
    }
}
