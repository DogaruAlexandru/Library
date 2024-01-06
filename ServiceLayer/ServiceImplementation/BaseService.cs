using log4net;
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
        private static readonly ILog log = LogManager.GetLogger(typeof(BookDomainServicesImplementation));

        public virtual void ValidateEntity<T>(T entity)
        {
            log.Debug($"Validating entity type: {typeof(T)}");

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
