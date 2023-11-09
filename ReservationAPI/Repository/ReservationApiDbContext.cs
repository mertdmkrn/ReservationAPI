using Microsoft.EntityFrameworkCore;
using ReservationAPI.Model;
using ReservationAPI.Helpers;

namespace ReservationAPI.Repository
{
    public class ReservationApiDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql(HelperMethods.GetConfiguration()["ConnectionStrings:AWSPostgreSQL"]);
        }

        public DbSet<Table> Tables { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
