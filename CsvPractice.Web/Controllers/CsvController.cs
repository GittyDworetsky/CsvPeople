using CsvHelper;
using CsvPractice.Data;
using CsvPractice.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text;

namespace CsvPractice.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CsvController : ControllerBase
    {
        private readonly string _connectionString;

        public CsvController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        [HttpGet]
        [Route("GeneratePeopleCsv")]
        public IActionResult GeneratePeopleCsv(int amount)
        {
            var people = Generate(amount);
            var peopleString = BuildPeopleCsv(people);
            return File(Encoding.UTF8.GetBytes(peopleString), "text/csv", "people.csv");


        }

        [HttpPost]
        [Route("uploadcsv")]
        public void UploadCsv(UploadsViewModel viewModel)
        {
            int firstComma = viewModel.Base64.IndexOf(',');
            string base64 = viewModel.Base64.Substring(firstComma + 1);
            var people = ReadCsv(base64);
            var repo = new CsvRepo(_connectionString);
            repo.AddPeople(people);
        }

        [HttpGet]
        [Route("getAll")]
        public List<Person> GetAll()
        {
            var repo = new CsvRepo(_connectionString);
            return repo.GetAll();
        }

        [HttpPost]
        [Route("deleteAll")]
        public void DeleteAll()
        {
            var repo = new CsvRepo(_connectionString);
            repo.DeleteAll();

        }





        private List<Person> Generate(int amount)
        {
            return Enumerable.Range(1, amount).Select(_ => new Person
            {
                FirstName = Faker.Name.First(),
                LastName = Faker.Name.Last(),
                Age = Faker.RandomNumber.Next(20, 100),
                Address = Faker.Address.StreetAddress(),
                Email = Faker.Internet.Email()
            }).ToList();
        }

        private string BuildPeopleCsv(List<Person> people)
        {
            var builder = new StringBuilder();
            var stringWriter = new StringWriter(builder);
            using var csv = new CsvWriter(stringWriter, CultureInfo.InvariantCulture);
            csv.WriteRecords(people);
            return builder.ToString();
        }

        private List<Person> ReadCsv(string base64File)
        {
            var bytes = Convert.FromBase64String(base64File);
            using var memoryStream = new MemoryStream(bytes);
            using var reader = new StreamReader(memoryStream);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<Person>().ToList();
        }



    }
}
