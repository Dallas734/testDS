using Microsoft.EntityFrameworkCore;
using testDS.Models;

namespace testDS.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<WeatherModel> Weathers { get; set; }
    }
}
