using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using CV_ASP.NET_LECT.Models;

namespace CV_ASP.NET_LECT.CustomValidation
{
    public class SalaryFromGreaterThenZero : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = validationContext.ObjectInstance as JobOffer;

            if (model.SalaryFrom <= 0)
                return new ValidationResult(GetErrorMessage(validationContext, true));
            else if (model.SalaryFrom >= model.SalaryTo)
                return new ValidationResult(GetErrorMessage(validationContext, false));

            return ValidationResult.Success;
        }

        private string GetErrorMessage(ValidationContext validationContext, bool isFirst)
        {
            // Message that was supplied
            if (!string.IsNullOrEmpty(this.ErrorMessage))
                return this.ErrorMessage;

            // Custom message
            if (!isFirst)
                return $"{validationContext.DisplayName} should be less than Salary To";
            return $"{validationContext.DisplayName} should be greater than zero";
        }
    }
}
