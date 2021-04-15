using GuaranteedRateHomework;
using GuaranteedRateHomework.Helpers;
using GuaranteedRateHomeworkAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IPersonRepository _personRepository;

        public PersonController(ILogger<PersonController> logger, IPersonRepository personRepository)
        {
            _logger = logger;
            _personRepository = personRepository;
        }

        [HttpGet ("name")]
        public async Task<ActionResult<IEnumerable<Person>>> GetPeopleByLastName()
        {
            return Ok(await _personRepository.GetPeopleByLastName());
        }

        [HttpGet("gender")]
        public async Task<ActionResult<IEnumerable<Person>>> GetPeopleByGender()
        {
            return Ok(await _personRepository.GetPeopleByGender());
        }

        [HttpGet("birthdate")]
        public async Task<ActionResult<IEnumerable<Person>>> GetPeopleByBirthdate()
        {
            return Ok(await _personRepository.GetPeopleByBirthdate());
        }

        [HttpPost]
        public async Task<ActionResult<Person>> CreateRecord([FromBody] string personString)
        {
            //filter the raw text from the input and make a person object
            Person pers = Filtering.CreatePersonFromString(personString);
            var success = await _personRepository.CreateRecord(pers);

            if (success)
                return pers;
            else
                return BadRequest("create record failed");
        }
    }
}
