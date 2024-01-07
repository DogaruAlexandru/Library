// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseService.cs" company="Transilvania University of Brasov">
//   Copyright (c) Dogaru Alexandru.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ServiceLayer.ServiceImplementation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using log4net;

    /// <summary>
    /// Represents a base service class providing common functionality for service implementations.
    /// </summary>
    public abstract class BaseService : IValidationService
    {
        /// <summary>
        /// Represents the logger instance for the <see cref="BaseService"/> class.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(BookDomainServicesImplementation));

        /// <summary>
        /// Validates the specified entity using Data Annotations.
        /// </summary>
        /// <typeparam name="T">The type of entity to be validated.</typeparam>
        /// <param name="entity">The entity to be validated.</param>
        public virtual void ValidateEntity<T>(T entity)
        {
            Log.Debug($"Validating entity type: {typeof(T)}");

            var validationContext = new ValidationContext(entity, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(entity, validationContext, validationResults, validateAllProperties: true);

            if (!isValid)
            {
                this.HandleValidationErrors(validationResults);
            }
        }

        /// <summary>
        /// Handles validation errors by throwing a <see cref="ValidationException"/>.
        /// </summary>
        /// <param name="validationResults">The list of validation results.</param>
        protected virtual void HandleValidationErrors(List<ValidationResult> validationResults)
        {
            foreach (var validationResult in validationResults)
            {
                throw new ValidationException(validationResult.ErrorMessage);
            }
        }
    }
}
