using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmarAPDChallenge.Models;

namespace SmarAPDChallenge.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<RoomModel> Rooms { get; set; }

        public DbSet<ScheduleModel> Schedules { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
