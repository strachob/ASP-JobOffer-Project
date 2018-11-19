using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using CV_ASP.NET_LECT.Models;

namespace CV_ASP.NET_LECT.CustomValidation
{
    public class SalaryToGreaterThenZero : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = validationContext.ObjectInstance as JobOffer;

            if (model.SalaryTo <= 0)
                return new ValidationResult(GetErrorMessage(validationContext));

            return ValidationResult.Success;
        }

        private string GetErrorMessage(ValidationContext validationContext)
        {
            // Message that was supplied
            if (!string.IsNullOrEmpty(this.ErrorMessage))
                return this.ErrorMessage;

            // Custom message
            return $"{validationContext.DisplayName} should be greater than zero";
        }
    }
}
