using GuaranteedRateHomework;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace GuaranteedRateHomeworkAPI.Data
{
    public class Seed
    {
        public static async Task<bool> SeedPeople(DataContext context, string path)
        {
            if (await context.People.AnyAsync()) return false;

            var userData = await System.IO.File.ReadAllTextAsync(path);
            var people = JsonSerializer.Deserialize<List<Person>>(userData);

            foreach (var pers in people)
            {
                pers.LastName = pers.LastName;
                pers.Gender = pers.Gender;
                pers.FirstName = pers.FirstName;
                pers.FavoriteColor = pers.FavoriteColor;
                pers.DateOfBirth = pers.DateOfBirth;
                context.People.Add(pers);
            }

            if (await context.SaveChangesAsync() > 1)
                return true;
            else
                return false;
        }
    }
}
