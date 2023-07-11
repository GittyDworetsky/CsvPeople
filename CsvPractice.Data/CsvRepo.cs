using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using Faker;
using Microsoft.EntityFrameworkCore;

namespace CsvPractice.Data
{
    public class CsvRepo
    {

        private readonly string _connectionString;

        public CsvRepo(string connectionString)
        {
            _connectionString = connectionString;
        }


        public void AddPeople(List<Person> people)
        {
            using var context = new CSVPracticeDataContext(_connectionString);
            context.AddRange(people);
            context.SaveChanges();

        }

        public void DeleteAll()
        {
            using var context = new CSVPracticeDataContext(_connectionString);
            context.Database.ExecuteSqlInterpolated($"Delete from People");
        }

        public List<Person> GetAll()
        {
            using var context = new CSVPracticeDataContext(_connectionString);
            return context.People.ToList();
        }
    }
}
