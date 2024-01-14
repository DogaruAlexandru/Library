// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Person.cs" company="Transilvania University of Brasov">
//   Copyright (c) Dogaru Alexandru.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DomainModel
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents different types of persons.
    /// </summary>
    public enum PersonType
    {
        /// <summary>
        /// Represents an individual who reads books.
        /// </summary>
        Reader,

        /// <summary>
        /// Represents personnel working in a library.
        /// </summary>
        LibraryPersonnel,
    }

    /// <summary>
    /// Represents a person in the domain model.
    /// </summary>
    public partial class Person
    {
        /// <summary>
        /// Gets or sets the unique identifier of the person.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the CNP (Personal Numerical Code) of the person.
        /// </summary>
        [Required(ErrorMessage = "The CNP cannot be null")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "CNP must be exactly 13 digits")]
        public string CNP { get; set; }

        /// <summary>
        /// Gets or sets the first name of the person.
        /// </summary>
        [Required(ErrorMessage = "The FirstName cannot be null")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The length must be between 1 and 100")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the person.
        /// </summary>
        [Required(ErrorMessage = "The LastName cannot be null")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The length must be between 1 and 100")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email address of the person.
        /// </summary>
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(100, ErrorMessage = "The length must be at most 100")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the person.
        /// </summary>
        [Phone(ErrorMessage = "Invalid phone number")]
        [StringLength(20, ErrorMessage = "The length must be at most 20")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the address of the person.
        /// </summary>
        [Required(ErrorMessage = "The Address cannot be null")]
        [StringLength(200, MinimumLength = 10)]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the type of the person (Reader or LibraryPersonnel).
        /// </summary>
        [Required(ErrorMessage = "The Type cannot be null")]
        [EnumDataType(typeof(PersonType))]
        public PersonType Type { get; set; }

        /// <summary>
        /// Gets a value indicating whether at least one of EmailAddress or PhoneNumber is provided.
        /// </summary>
        [Required]
        [Range(typeof(bool), "true", "true", ErrorMessage = "At least one of EmailAddress or PhoneNumber should not be null")]
        public bool IsEmailOrPhoneNumberProvided => !string.IsNullOrEmpty(this.EmailAddress) || !string.IsNullOrEmpty(this.PhoneNumber);
    }
}
