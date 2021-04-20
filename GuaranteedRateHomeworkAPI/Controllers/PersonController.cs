using GuaranteedRateHomework;
using GuaranteedRateHomework.Helpers;
using GuaranteedRateHomeworkAPI.Data;
using GuaranteedRateHomeworkAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaranteedRateHomeworkAPI.Controllers
{
    [ApiController]
    [Route("records")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly DataContext _context;
        private readonly IPersonRepository _repo;

        public PersonController(DataContext personRepository, ILogger<PersonController> logger, IPersonRepository repo)
        {
            _logger = logger;
            _context = personRepository;
            _repo = repo;
        }

        [HttpGet ("name")]
        public async Task<ActionResult<IEnumerable<Person>>> GetPeopleByLastName()
        {
            return Ok(await _repo.GetPeopleByLastName());
        }

        [HttpGet("gender")]
        public async Task<ActionResult<IEnumerable<Person>>> GetPeopleByGender()
        {
            return Ok(await _repo.GetPeopleByGender());
        }

        [HttpGet("birthdate")]
        public async Task<ActionResult<IEnumerable<Person>>> GetPeopleByBirthdate()
        {
            return Ok(await _repo.GetPeopleByBirthdate());
        }

        [HttpPost]
        public async Task<ActionResult<Person>> CreateRecord([FromBody] string personString)
        {
            //filter the raw text from the input and make a person object
            Person pers = Filtering.CreatePersonFromString(personString);
            var success = await _repo.CreateRecord(pers);

            if (success)
                return pers;
            else
                return BadRequest("Attempt to create new person record failed");
        }

        private bool personExists(Person pers)
        {
            var result = _context.People.Where(x => x.LastName == pers.LastName)
                                        .Where(x => x.FirstName == pers.FirstName)
                                        .Where(x => x.DateOfBirth == pers.DateOfBirth);

            if (result.Any())
                return true;
            else
                return false;
        }
    }
}
