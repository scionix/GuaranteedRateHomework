using GuaranteedRateHomework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuaranteedRateHomeworkAPI.Interfaces
{
    public interface IPersonRepository
    {
        Task<bool> CreateRecord(Person pers);
        Task<IEnumerable<Person>> GetPeopleByLastName();
        Task<IEnumerable<Person>> GetPeopleByGender();
        Task<IEnumerable<Person>> GetPeopleByBirthdate();
    }
}
