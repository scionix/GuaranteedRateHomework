using GuaranteedRateHomework;
using GuaranteedRateHomework.Helpers;
using GuaranteedRateHomeworkAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuaranteedRateHomeworkAPI.Controllers
{
    [ApiController]
    [Route("records")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly DataContext _context;

        public PersonController(DataContext personRepository, ILogger<PersonController> logger)
        {
            _logger = logger;
            _context = personRepository;
        }

        [HttpGet ("name")]
        public async Task<ActionResult<IEnumerable<Person>>> GetPeopleByLastName()
        {
            var people = await _context.People.ToListAsync();
            people = (List<Person>)Sorting.LastnameSort(people);

            return people;
        }

        [HttpGet("gender")]
        public async Task<ActionResult<IEnumerable<Person>>> GetPeopleByGender()
        {
            var people = await _context.People.ToListAsync();
            people = (List<Person>)Sorting.GenderSort(people);

            return people;
        }

        [HttpGet("birthdate")]
        public async Task<ActionResult<IEnumerable<Person>>> GetPeopleByBirthdate()
        {
            var people = await _context.People.ToListAsync();
            people = (List<Person>)Sorting.BirthdateSort(people);

            return people;
        }

        [HttpPost]
        public async Task<ActionResult<Person>> CreateRecord([FromBody] string personString)
        {
            //filter the raw text from the input and make a person object
            Person pers = Filtering.CreatePersonFromString(personString);

            //check if the person we got from the body is properly formatted
            if (pers.FavoriteColor == null)
            {
                _logger.LogWarning("CreateRecord() call failed to create a new person record due to improperly formatted person");
                return BadRequest("Attempt to create new record failed");
            }
            else
            {
                _context.Add(pers);

                //sanity check that the db updated
                if (await _context.SaveChangesAsync() > 0)
                    return pers;
                else
                {
                    _logger.LogWarning("CreateRecord() call failed");
                    return BadRequest("Attempt to create new record failed");
                }
            }
        }
    }
}
