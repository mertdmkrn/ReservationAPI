using ReservationAPI.Handler.Model;

namespace ReservationAPI.Handler.Abstract
{
    public interface IMailHandler
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
