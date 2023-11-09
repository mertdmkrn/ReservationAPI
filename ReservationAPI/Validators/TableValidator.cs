using FluentValidation;

namespace ReservationAPI.Model
{
    public class TableValidator : AbstractValidator<Table>
    {
        public TableValidator()
        {
            RuleFor(t => t.Number).NotEmpty().WithMessage("Masa numarası girilmelidir.").MaximumLength(6).WithMessage("Masa numarası 6 karakterden fazla girilmemelidir.");
            RuleFor(t => t.Capacity).NotEmpty().WithMessage("Masa kapasitesi girilmelidir.").GreaterThan(1).WithMessage("Masa kapasitesi 1'den büyük olmalıdır.");
        }
    }
}