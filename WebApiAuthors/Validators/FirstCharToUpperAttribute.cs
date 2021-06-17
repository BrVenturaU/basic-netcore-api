using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAuthors.Validators
{
    public class FirstCharToUpperAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(value.ToString()) || value == null)
                return ValidationResult.Success;

            ErrorMessage = string.IsNullOrEmpty(ErrorMessage) ? "The first char of the name must be uppercase"
                : ErrorMessageString;


            string name = value.ToString();
            string firstCharName = name[0].ToString();

            if (firstCharName != firstCharName.ToUpper())
                return new ValidationResult(ErrorMessage);
            return ValidationResult.Success;
            
        }
    }
}
