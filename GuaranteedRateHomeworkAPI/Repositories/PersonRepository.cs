using GuaranteedRateHomeworkAPI.Data;
using GuaranteedRateHomeworkAPI.Entities;
using GuaranteedRateHomeworkAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuaranteedRateHomeworkAPI.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ILogger<PersonRepository> _logger;
        private readonly DataContext _context;

        public PersonRepository(ILogger<PersonRepository> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IEnumerable<Person>> GetPeopleByBirthdate()
        {
            var list = await _context.People.ToListAsync();
            return ListSorting.BirthdateSort(list);
        }

        public async Task<IEnumerable<Person>> GetPeopleByGender()
        {
            var list = await _context.People.ToListAsync();
            return ListSorting.GenderSort(list);
        }

        public async Task<IEnumerable<Person>> GetPeopleByLastName()
        {
            var list = await _context.People.ToListAsync();
            return ListSorting.LastnameSort(list);
        }

        public async Task<bool> CreateRecord(Person pers)
        {
            _context.Add(pers);
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}
