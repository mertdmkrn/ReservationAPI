using ReservationAPI.Model;
using ReservationAPI.Repository.Abstract;
using ReservationAPI.Repository.Concrete;
using ReservationAPI.Service.Abstract;

namespace ReservationAPI.Service.Concrete
{
    public class ReservationService : IReservationService
    {
        private IReservationRepository _reservationRepository;

        public ReservationService()
        {
            _reservationRepository = new ReservationRepository();
        }

        public async Task<Reservation> GetReservationIdAsync(Guid id)
        {
            return await _reservationRepository.GetReservationIdAsync(id);
        }

        public async Task<List<Reservation>> GetReservationsByDateAsync(DateTime date)
        {
            return await _reservationRepository.GetReservationsByDateAsync(date.Date);
        }

        public async Task<Reservation> SaveReservationAsync(Reservation reservation)
        {
            return await _reservationRepository.SaveReservationAsync(reservation);
        }

        public async Task<bool> DeleteReservationByIdAsync(Guid id)
        {
            return await _reservationRepository.DeleteReservationByIdAsync(id);
        }
    }
}
