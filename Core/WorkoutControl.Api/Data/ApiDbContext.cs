using Microsoft.EntityFrameworkCore;
using WorkoutControl.Api.Models;

namespace WorkoutControl.Api.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
    }
}