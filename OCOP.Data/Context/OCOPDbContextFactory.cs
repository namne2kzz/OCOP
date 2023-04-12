using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OCOP.Data.Context
{
    public class OCOPDbContextFactory : IDesignTimeDbContextFactory<OCOPDbContext>
    {
        public OCOPDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("OCOPSolutionDb");

            var optionsBuilder = new DbContextOptionsBuilder<OCOPDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new OCOPDbContext(optionsBuilder.Options);
        }
    }
}
