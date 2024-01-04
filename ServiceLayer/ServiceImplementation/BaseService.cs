using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceImplementation
{
    public abstract class BaseService : IValidationService
    {
        public virtual void ValidateEntity<T>(T entity)
        {
            var validationContext = new ValidationContext(entity, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(entity, validationContext, validationResults, validateAllProperties: true);

            if (!isValid)
            {
                HandleValidationErrors(validationResults);
            }
        }

        protected virtual void HandleValidationErrors(List<ValidationResult> validationResults)
        {
            foreach (var validationResult in validationResults)
            {
                throw new ValidationException(validationResult.ErrorMessage);
            }
        }
    }
}
