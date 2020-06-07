using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using VehicleRunsheetMBA.Models;

namespace VehicleRunsheetMBAProj.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(x => x.OrderNumber).NotEmpty().MinimumLength(4).MaximumLength(6);
        }
    }
}
