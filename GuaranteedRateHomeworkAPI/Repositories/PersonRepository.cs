using GuaranteedRateHomework;
using GuaranteedRateHomeworkAPI.Data;
using GuaranteedRateHomeworkAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
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
            return Sorting.BirthdateSort(list);
        }

        public async Task<IEnumerable<Person>> GetPeopleByGender()
        {
            var list = await _context.People.ToListAsync();
            return Sorting.GenderSort(list);
        }

        public async Task<IEnumerable<Person>> GetPeopleByLastName()
        {
            var list = await _context.People.ToListAsync();
            return Sorting.LastnameSort(list);
        }

        public async Task<bool> CreateRecord(Person pers)
        {
            ///check if the person we got from the body is properly formatted
            if (pers.FavoriteColor == null)
                _logger.LogWarning("CreateRecord() call failed to create a new person record due to improperly formatted person");
            else
            {
                if (!PersonExists(pers))
                    _context.Add(pers);
                else
                    _logger.LogWarning("CreateRecord() tried to add duplicate person");

                ///sanity check that the db updated
                if (await _context.SaveChangesAsync() > 0)
                    return true;
                else
                    _logger.LogWarning("CreateRecord() call failed");
            }

            ///returns true if the Db updated
            return (await _context.SaveChangesAsync() > 0);
        }

        public bool PersonExists(Person pers)
        {
            ///check if there is anyone with an identical Firstname, Lastname, and DoB in the Database
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
