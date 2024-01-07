// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IValidationService.cs" company="Your Company">
//   Copyright (c) Your Company. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ServiceLayer
{
    using System;

    /// <summary>
    /// Represents the service interface for entity validation.
    /// </summary>
    /// <remarks>
    /// This interface provides a method for validating entities of type T.
    /// </remarks>
    public interface IValidationService
    {
        /// <summary>
        /// Validates an entity of type T.
        /// </summary>
        /// <typeparam name="T">The type of the entity to be validated.</typeparam>
        /// <param name="entity">The entity to be validated.</param>
        void ValidateEntity<T>(T entity);
    }
}
