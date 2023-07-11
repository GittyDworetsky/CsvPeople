﻿using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvPractice.Data
{
    public class CsvPracticeContextFactory : IDesignTimeDbContextFactory<CSVPracticeDataContext>
    {
        
            public CSVPracticeDataContext CreateDbContext(string[] args)
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), $"..{Path.DirectorySeparatorChar}CsvPractice.Web"))
                    .AddJsonFile("appsettings.json")
                    .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true).Build();

                return new CSVPracticeDataContext(config.GetConnectionString("ConStr"));
            }
        

    }
}
