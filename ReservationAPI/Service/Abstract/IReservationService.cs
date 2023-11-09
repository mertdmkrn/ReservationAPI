using ReservationAPI.Model;

namespace ReservationAPI.Service.Abstract
{
    public interface IReservationService
    {
        Task<Reservation> GetReservationIdAsync(Guid id);
        Task<List<Reservation>> GetReservationsByDateAsync(DateTime date);
        Task<Reservation> SaveReservationAsync(Reservation reservation);
        Task<bool> DeleteReservationByIdAsync(Guid id);
    }
}
