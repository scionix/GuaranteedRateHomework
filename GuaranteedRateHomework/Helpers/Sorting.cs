using System.Collections.Generic;
using System.Linq;

namespace GuaranteedRateHomework
{
    public class Sorting
    {
        public static IEnumerable<Person> GenderSort(IEnumerable<Person> personList)
        {
            List<Person> genderSorted = personList.OrderBy(o => o.Gender)
                                                  .ThenBy(o => o.LastName)
                                                  .ThenBy(o => o.FirstName)
                                                  .ToList();
            return genderSorted;
        }
        public static IEnumerable<Person> BirthdateSort(IEnumerable<Person> personList)
        {
            List<Person> birthdateSorted = personList.OrderBy(o => o.DateOfBirth)
                                                     .ThenBy(o => o.LastName)
                                                     .ThenBy(o => o.FirstName)
                                                     .ToList();
            return birthdateSorted;
        }

        public static IEnumerable<Person> LastnameSort(IEnumerable<Person> personList)
        {
            List<Person> lastnameSorted = personList.OrderByDescending(o => o.LastName)
                                                    .ThenByDescending(o => o.FirstName)
                                                    .ToList();
            return lastnameSorted;
        }
    }
}
