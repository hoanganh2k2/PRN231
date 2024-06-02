using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using ReservarionWebAPI.Models;
using ReservarionWebAPI.Repositories;

namespace ReservarionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IRepository repository;

        private readonly IWebHostEnvironment webHostEnvironment;

        public ReservationController(IRepository repo, IWebHostEnvironment environment)
        {
            repository = repo;
            webHostEnvironment = environment;
        }

        [HttpGet]
        public IEnumerable<Reservation> Get() => repository.Reservations;

        [HttpGet("{id}")]
        public ActionResult<Reservation> Get(int id)
        {
            if (id == 0) return BadRequest("Value must be passed in the request body");

            return Ok(repository[id]);
        }

        [HttpPost]
        public Reservation Post([FromBody] Reservation res) =>
            repository.AddReservation(new Reservation
            {
                Name = res.Name,
                StartLocation = res.StartLocation,
                EndLocation = res.EndLocation
            });

        [HttpPut]
        public Reservation Put([FromBody] Reservation res) => repository.UpdateReservation(res);

        [HttpDelete("{id}")]
        public void Delete(int id) => repository.DeleteReservation(id);

        bool Authenticate()
        {
            string[] allowedKeys = new[] { "Secret@123", "Secret#12", "SecretABC" };
            StringValues key = Request.Headers["Key"];
            int count = (from t in allowedKeys where t == key select t).Count();
            return count != 0;
        }

        [HttpPost("{PostXml}")]
        [Consumes("application/xml")]
        public Reservation PostXml([FromBody] System.Xml.Linq.XElement res)
        {
            return repository.AddReservation(new Reservation
            {
                Name = res.Element("Name").Value,
                StartLocation = res.Element("StartLocation").Value,
                EndLocation = res.Element("EndLocation").Value
            });
        }

        [HttpGet("ShowReservation.{fomart}"), FormatFilter]
        public IEnumerable<Reservation> ShowReservation() => repository.Reservations;

    }
}
