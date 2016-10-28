using System;
using System.Collections.Generic;
using System.Linq;
using RAP.API.Models;
using RAP.Core.DataModels;
using FluentValidation;
using FluentValidation.Results;

namespace RAP.API
{
    public class WorkflowDTOValidator : AbstractValidator<WorkflowDTO>
    {
        public WorkflowDTOValidator()
        {
            //action should be once char length
            RuleFor(item => item.Action).Length(1).WithMessage("Invalid Action.");
            //comment required for Actions other than Approve
            RuleFor(item => item.Comments).NotEmpty().When(when => when.Action != "A").WithMessage("Comments required.");
            //new status required for Action Return
            RuleFor(item => item.NewStatus).NotNull().When(when => when.Action == "R").WithMessage("Invalid return status.");
            //new status not applicable for Actions other than Return
            When(item => item.NewStatus != null, () =>
            {
                RuleFor(item => item.Action).Equal("R").WithMessage("NewStatus is not applicable");
            });
        }
    }
}