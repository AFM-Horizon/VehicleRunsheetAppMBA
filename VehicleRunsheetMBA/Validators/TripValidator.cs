using FluentValidation;
using VehicleRunsheetMBA.Models;

namespace VehicleRunsheetMBAProj.Validators
{
    public class TripValidator : AbstractValidator<Trip>
    {
        public TripValidator()
        {
            RuleFor(x => x.ReceivedBy).NotEmpty();
            RuleFor(x => x.Customer).NotEmpty();
            RuleFor(x => x.Orders).NotEmpty().WithMessage("You must supply at least one order code");
        }
    }
}