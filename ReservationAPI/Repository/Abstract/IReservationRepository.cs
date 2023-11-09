using ReservationAPI.Model;

namespace ReservationAPI.Repository.Abstract
{
    public interface IReservationRepository
    {
        Task<Reservation> GetReservationIdAsync(Guid id);
        Task<List<Reservation>> GetReservationsByDateAsync(DateTime date);
        Task<Reservation> SaveReservationAsync(Reservation reservation);
        Task<bool> DeleteReservationByIdAsync(Guid id);
    }
}
