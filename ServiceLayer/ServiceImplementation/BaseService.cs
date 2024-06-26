﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseService.cs" company="Transilvania University of Brasov">
//   Copyright (c) Dogaru Alexandru.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ServiceLayer.ServiceImplementation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using DomainModel;
    using log4net;
    using Newtonsoft.Json;

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
            bool isValid = Validator.TryValidateObject(entity, validationContext, validationResults, validateAllProperties: false);

            if (!isValid)
            {
                throw new ValidationException(validationResults[0].ErrorMessage);
            }
        }

        /// <summary>
        /// Reads a configuration file and retrieves the value associated with the specified field name.
        /// </summary>
        /// <typeparam name="T">The type of the value to retrieve.</typeparam>
        /// <param name="fieldName">The name of the field whose value is to be retrieved.</param>
        /// <returns>The value associated with the specified field name.</returns>
        public T GetValueFromConfig<T>(string fieldName)
        {
            string jsonFilePath = "D:\\School\\Sem1\\ASSE\\Tema\\Library\\Library\\config.json";
            string jsonData = File.ReadAllText(jsonFilePath);
            dynamic configData = JsonConvert.DeserializeObject(jsonData);
            return configData[fieldName].ToObject<T>();
        }
    }
}