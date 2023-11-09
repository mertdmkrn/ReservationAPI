using Microsoft.EntityFrameworkCore;
using ReservationAPI.Model;
using ReservationAPI.Repository.Abstract;

namespace ReservationAPI.Repository.Concrete
{
    public class ReservationRepository : IReservationRepository
    {
        public async Task<Reservation> GetReservationIdAsync(Guid id)
        {
            using (var context = new ReservationApiDbContext())
            {
                return await context.Reservations
                    .FindAsync(id);
            }
        }

        public async Task<List<Reservation>> GetReservationsByDateAsync(DateTime date)
        {
            using (var context = new ReservationApiDbContext())
            {
                return await context.Reservations
                    .AsNoTracking()
                    .Where(x => x.Date.Equals(date))
                    .ToListAsync();
            }
        }

        public async Task<Reservation> SaveReservationAsync(Reservation reservation)
        {
            using (var context = new ReservationApiDbContext())
            {
                reservation.Date = reservation.Date.Date;

                await context.Reservations.AddAsync(reservation);
                await context.SaveChangesAsync();
                return reservation;
            }
        }

        public async Task<bool> DeleteReservationByIdAsync(Guid id)
        {
            using (var context = new ReservationApiDbContext())
            {
                await context.Reservations
                    .Where(x => x.Id == id)
                    .ExecuteDeleteAsync();
                return true;
            }
        }
    }
}
