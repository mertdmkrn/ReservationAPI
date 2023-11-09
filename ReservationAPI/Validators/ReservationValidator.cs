using FluentValidation;
using ReservationAPI.Model;

namespace ReservationAPI.Validators
{
    public class ReservationValidator : AbstractValidator<Reservation>
    {
        public ReservationValidator()
        {
            RuleFor(r => r.Date).NotEmpty().WithMessage("Reservasyon tarihi girilmeli.");
            RuleFor(r => r.CustomerName).NotEmpty().WithMessage("Müşteri adı girilmeli.").MaximumLength(100).WithMessage("Müşteri adı 100 karakterden fazla girilmemelidir.");
            RuleFor(r => r.CustomerEmail).NotEmpty().WithMessage("Müşteri maili girilmeli").MaximumLength(100).WithMessage("Müşteri maili 100 karakterden fazla girilmemelidir.");
            RuleFor(r => r.Description).MaximumLength(200).WithMessage("Açıklama 200 karakterden fazla girilmemelidir.");
            RuleFor(r => r.GuestCount).NotEmpty().WithMessage("Müşteri sayısı girilmelidir.").GreaterThan(0).WithMessage(errorMessage: "Müşteri sayısı girilmelidir.");
        }
    }
}