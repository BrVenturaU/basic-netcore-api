using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApiAuthors.Database;
using WebApiAuthors.Entities;

namespace WebApiAuthors.Validators
{
    public class ExistsNameAttribute : ValidationAttribute
    {
        private readonly Type _entityType;

        public ExistsNameAttribute(Type entityType)
        {
            _entityType = entityType;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(value.ToString()) || value == null)
                return ValidationResult.Success;

            string name = value.ToString();
            var context = (DataContext)validationContext.GetService(typeof(DataContext));
            ValidateEntityValues(context.Model.GetEntityTypes());
            return ValueExists(context, name);
        }

        private ValidationResult ValueExists(DataContext context, string value)
        {
            if (_entityType.Name == "Author" && context.Authors.Any(a => a.Name == value))
                return new ValidationResult("The field Name is already taken.");
            return ValidationResult.Success;
        }

        private void ValidateEntityValues(IEnumerable<IEntityType> entityTypes)
        {
            if (!entityTypes.Any(et => et.ClrType.Name == _entityType.Name))
                throw new ArgumentException($"The entity given name {_entityType.Name} doesn't exists");

        }


    }
}
