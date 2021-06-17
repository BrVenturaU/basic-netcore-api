using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApiAuthors.Database;

namespace WebApiAuthors.Validators
{
    public class ExistsNameAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(value.ToString()) || value == null)
                return ValidationResult.Success;

            string name = value.ToString();
            var context = (DataContext) validationContext.GetService(typeof(DataContext));
            var existsName = context.Authors.Any(a => a.Name == name);
            if (existsName)
                return new ValidationResult("The field name is already taken");
            return ValidationResult.Success;
        }
    }
}
