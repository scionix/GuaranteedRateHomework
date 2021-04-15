﻿using GuaranteedRateHomework;
using System.Collections.Generic;
using System.Linq;

namespace GuaranteedRateHomeworkAPI.Repositories
{
    public class ListSorting
    {
        public static List<Person> GenderSort(List<Person> personList)
        {
            List<Person> genderSorted = personList.OrderBy(o => o.Gender)
                                                  .ThenBy(o => o.LastName)
                                                  .ThenBy(o => o.FirstName)
                                                  .ToList();
            return genderSorted;
        }
        public static List<Person> BirthdateSort(List<Person> personList)
        {
            List<Person> birthdateSorted = personList.OrderBy(o => o.DateOfBirth)
                                                     .ThenBy(o => o.LastName)
                                                     .ThenBy(o => o.FirstName)
                                                     .ToList();
            return birthdateSorted;
        }

        public static List<Person> LastnameSort(List<Person> personList)
        {
            List<Person> lastnameSorted = personList.OrderByDescending(o => o.LastName)
                                                    .ThenByDescending(o => o.FirstName)
                                                    .ToList();
            return lastnameSorted;
        }
    }
}
