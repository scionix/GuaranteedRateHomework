using GuaranteedRateHomeworkAPI.Data;
using GuaranteedRateHomeworkAPI.Entities;
using GuaranteedRateHomeworkAPI.Interfaces;
using GuaranteedRateHomeworkAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaranteedRateHomeworkAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonRepository _personRepository;

        public PersonController(ILogger<PersonController> logger, IPersonRepository personRepository)
        {
            _logger = logger;
            _personRepository = personRepository;
        }

        [HttpGet ("{name}")]
        public async Task<ActionResult<IEnumerable<Person>>> GetPeopleByLastName()
        {
            return Ok(await _personRepository.GetPeopleByLastName());
        }

        [HttpGet("{gender}")]
        public async Task<ActionResult<IEnumerable<Person>>> GetPeopleByGender(string gender)
        {
            return Ok(await _personRepository.GetPeopleByGender());
        }

        [HttpGet("{birthdate}")]
        public async Task<ActionResult<IEnumerable<Person>>> GetPeopleByBirthdate()
        {
            return Ok(await _personRepository.GetPeopleByBirthdate());
        }

        [HttpPost("{records}")]
        public async Task<ActionResult<Person>> CreateRecord([FromBody] Person pers)
        {
            var success = await _personRepository.CreateRecord(pers);

            if (success)
                return pers;
            else
                return BadRequest("create record failed");
        }
    }
}
