using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Use your PostgreSQL connection string
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=resilienceclinic;Username=postgres;Password=Keonjhar@1618");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
