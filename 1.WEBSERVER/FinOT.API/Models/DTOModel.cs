using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;
using RAP.API.Common;
using RAP.Core.DataModels;


namespace RAP.API.Models
{

    public class ReportPage
    {
        public string PageTitle { get;set; }
        public string Source { get; set; }
    }

    public class GetDivisionDTO
    {
        public int FY { get; set; }
        public int? RestructureID { get; set; }
    }

    public class GetBureauDTO
    {
        public int FY { get; set; }
        public string Division { get; set; }
        public int? RestructureID { get; set; }
    }

    public class GetProgramDTO
    {
        public int FY { get; set; }
        public string Bureau { get; set; }
        public int? RestructureID { get; set; }
    }

    public class IDCodeDTO
    {
        public int ID { get; set; }
        public string Code { get; set; }
    }

    public class FYCodeDTO
    {
        public List<int> FY { get; set; }
        public string Code { get; set; }
    }

    public class WorkflowDTO : IValidatableObject
    {
        private readonly IValidator _validator;

        public int ReqID { get; set; }
        public string Action { get; set; }
        public IDDescription NewStatus { get; set; }
        public string Comments { get; set; }

        public WorkflowDTO()
        {
            _validator = new WorkflowDTOValidator();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            return _validator.Validate(this).ToValidationResult();
        }
    }

}