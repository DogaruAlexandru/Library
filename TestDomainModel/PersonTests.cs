// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PersonTests.cs" company="Transilvania University of Brasov">
//   Copyright (c) Dogaru Alexandru.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TestDomainModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DomainModel;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Represents a set of tests for the Person class in the domain model.
    /// </summary>
    [TestClass]
    public class PersonTests
    {
        /// <summary>
        /// Verifies that the Id property of the Book class is greater than zero.
        /// </summary>
        [TestMethod]
        public void IdShouldBeGreaterThanZero()
        {
            // Arrange
            var person = new Person();

            // Act
            person.Id = 1; // Set a valid Id

            // Assert
            Assert.IsTrue(person.Id > 0);
        }

        /// <summary>
        /// Verifies that a valid CNP in the Person object passes validation.
        /// </summary>
        [TestMethod]
        public void ValidCNPShouldPassValidation()
        {
            // Arrange
            var validPerson = new Person
            {
                CNP = "1234567890123", // A valid CNP
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(validPerson.CNP, new ValidationContext(validPerson) { MemberName = nameof(Person.CNP) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count == 0);
        }

        /// <summary>
        /// Verifies that an invalid CNP in the Person object fails validation.
        /// </summary>
        [TestMethod]
        public void InvalidCNPShouldFailValidation()
        {
            // Arrange
            var invalidPerson = new Person
            {
                CNP = "InvalidCNP", // An invalid CNP
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(invalidPerson.CNP, new ValidationContext(invalidPerson) { MemberName = nameof(Person.CNP) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count > 0);
        }

        /// <summary>
        /// Verifies that a CNP with letters of size 13 in the Person object fails validation.
        /// </summary>
        [TestMethod]
        public void CNPWithLettersShouldFail()
        {
            // Arrange
            var person = new Person
            {
                CNP = "12345678901AB", // CNP with non-numeric characters
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(person.CNP, new ValidationContext(person) { MemberName = nameof(Person.CNP) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count > 0);
        }

        /// <summary>
        /// Verifies that a CNP with extra spaces in the Person object fails validation.
        /// </summary>
        [TestMethod]
        public void CNPWithExtraSpacesShouldFailValidation()
        {
            // Arrange
            var person = new Person
            {
                CNP = " 1234567890123 ", // CNP with extra spaces
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(person.CNP, new ValidationContext(person) { MemberName = nameof(Person.CNP) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count > 0);
        }

        /// <summary>
        /// Verifies that an empty CNP in the Person object fails validation.
        /// </summary>
        [TestMethod]
        public void EmptyCNPShouldFailValidation()
        {
            // Arrange
            var person = new Person
            {
                CNP = string.Empty,
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(person.CNP, new ValidationContext(person) { MemberName = nameof(Person.CNP) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count > 0);
        }

        /// <summary>
        /// Verifies that a CNP with less than 13 digits in the Person object fails validation.
        /// </summary>
        [TestMethod]
        public void CNPLessThan13DigitsShouldFailValidation()
        {
            // Arrange
            var person = new Person
            {
                CNP = "123456789012", // CNP with 12 digits
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(person.CNP, new ValidationContext(person) { MemberName = nameof(Person.CNP) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count > 0);
        }

        /// <summary>
        /// Verifies that a CNP with more than 13 digits in the Person object fails validation.
        /// </summary>
        [TestMethod]
        public void CNPMoreThan13DigitsShouldFailValidation()
        {
            // Arrange
            var person = new Person
            {
                CNP = "123456789012345", // CNP with 15 digits
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(person.CNP, new ValidationContext(person) { MemberName = nameof(Person.CNP) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count > 0);
        }

        /// <summary>
        /// Verifies that a valid first name in the Person object passes validation.
        /// </summary>
        [TestMethod]
        public void ValidFirstNameShouldPassValidation()
        {
            // Arrange
            var person = new Person
            {
                FirstName = "John", // Set a valid first name
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(person.FirstName, new ValidationContext(person) { MemberName = nameof(Person.FirstName) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count == 0);
        }

        /// <summary>
        /// Verifies that a null first name in the Person object fails validation.
        /// </summary>
        [TestMethod]
        public void NullFirstNameShouldFailValidation()
        {
            // Arrange
            var person = new Person
            {
                FirstName = null, // Set a null first name
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(person.FirstName, new ValidationContext(person) { MemberName = nameof(Person.FirstName) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count > 0);
        }

        /// <summary>
        /// Verifies that an empty first name in the Person object fails validation.
        /// </summary>
        [TestMethod]
        public void EmptyFirstNameShouldFailValidation()
        {
            // Arrange
            var person = new Person
            {
                FirstName = string.Empty, // Set an empty first name
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(person.FirstName, new ValidationContext(person) { MemberName = nameof(Person.FirstName) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count > 0);
        }

        /// <summary>
        /// Verifies that a first name exceeding the maximum length in the Person object fails validation.
        /// </summary>
        [TestMethod]
        public void LongFirstNameShouldFailValidation()
        {
            // Arrange
            var person = new Person
            {
                FirstName = new string('A', 101), // Set a first name exceeding the maximum length
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(person.FirstName, new ValidationContext(person) { MemberName = nameof(Person.FirstName) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count > 0);
        }

        /// <summary>
        /// Verifies that a valid last name in the Person object passes validation.
        /// </summary>
        [TestMethod]
        public void ValidLastNameShouldPassValidation()
        {
            // Arrange
            var person = new Person
            {
                LastName = "Doe", // Set a valid last name
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(person.LastName, new ValidationContext(person) { MemberName = nameof(Person.LastName) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count == 0);
        }

        /// <summary>
        /// Verifies that a null last name in the Person object fails validation.
        /// </summary>
        [TestMethod]
        public void NullLastNameShouldFailValidation()
        {
            // Arrange
            var person = new Person
            {
                LastName = null, // Set a null last name
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(person.LastName, new ValidationContext(person) { MemberName = nameof(Person.LastName) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count > 0);
        }

        /// <summary>
        /// Verifies that an empty last name in the Person object fails validation.
        /// </summary>
        [TestMethod]
        public void EmptyLastNameShouldFailValidation()
        {
            // Arrange
            var person = new Person
            {
                LastName = string.Empty, // Set an empty last name
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(person.LastName, new ValidationContext(person) { MemberName = nameof(Person.LastName) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count > 0);
        }

        /// <summary>
        /// Verifies that a last name exceeding the maximum length in the Person object fails validation.
        /// </summary>
        [TestMethod]
        public void LongLastNameShouldFailValidation()
        {
            // Arrange
            var person = new Person
            {
                LastName = new string('D', 101), // Set a last name exceeding the maximum length
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(person.LastName, new ValidationContext(person) { MemberName = nameof(Person.LastName) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count > 0);
        }

        /// <summary>
        /// Verifies that a valid email address in the Person object passes validation.
        /// </summary>
        [TestMethod]
        public void ValidEmailAddressShouldPassValidation()
        {
            // Arrange
            var person = new Person
            {
                EmailAddress = "john.doe@example.com", // Set a valid email address
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(person.EmailAddress, new ValidationContext(person) { MemberName = nameof(Person.EmailAddress) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count == 0);
        }

        /// <summary>
        /// Verifies that a null email address in the Person object passes validation (assuming it's optional).
        /// </summary>
        [TestMethod]
        public void NullEmailAddressShouldPassValidation()
        {
            // Arrange
            var person = new Person
            {
                EmailAddress = null, // Set a null email address
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(person.EmailAddress, new ValidationContext(person) { MemberName = nameof(Person.EmailAddress) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count == 0);
        }

        /// <summary>
        /// Verifies that an empty email address in the Person object fails validation.
        /// </summary>
        [TestMethod]
        public void EmptyEmailAddressShouldFailValidation()
        {
            // Arrange
            var person = new Person
            {
                EmailAddress = string.Empty, // Set an empty email address
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(person.EmailAddress, new ValidationContext(person) { MemberName = nameof(Person.EmailAddress) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count > 0);
        }

        /// <summary>
        /// Verifies that an invalid email address in the Person object fails validation.
        /// </summary>
        [TestMethod]
        public void InvalidEmailAddressShouldFailValidation()
        {
            // Arrange
            var person = new Person
            {
                EmailAddress = "invalid-email", // Set an invalid email address
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(person.EmailAddress, new ValidationContext(person) { MemberName = nameof(Person.EmailAddress) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count > 0);
        }

        /// <summary>
        /// Verifies that a long email address in the Person object fails validation.
        /// </summary>
        [TestMethod]
        public void LongEmailAddressShouldFailValidation()
        {
            // Arrange
            var person = new Person
            {
                EmailAddress = new string('a', 101) + "@example.com", // Set an email address exceeding the maximum length
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(person.EmailAddress, new ValidationContext(person) { MemberName = nameof(Person.EmailAddress) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count > 0);
        }

        /// <summary>
        /// Verifies that a valid phone number in the Person object passes validation.
        /// </summary>
        [TestMethod]
        public void ValidPhoneNumberShouldPassValidation()
        {
            // Arrange
            var person = new Person
            {
                PhoneNumber = "123-456-7890", // Set a valid phone number
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(person.PhoneNumber, new ValidationContext(person) { MemberName = nameof(Person.PhoneNumber) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count == 0);
        }

        /// <summary>
        /// Verifies that a null phone number in the Person object passes validation (assuming it's optional).
        /// </summary>
        [TestMethod]
        public void NullPhoneNumberShouldPassValidation()
        {
            // Arrange
            var person = new Person
            {
                PhoneNumber = null, // Set a null phone number
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(person.PhoneNumber, new ValidationContext(person) { MemberName = nameof(Person.PhoneNumber) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count == 0);
        }

        /// <summary>
        /// Verifies that an empty phone number in the Person object fails validation.
        /// </summary>
        [TestMethod]
        public void EmptyPhoneNumberShouldFailValidation()
        {
            // Arrange
            var person = new Person
            {
                PhoneNumber = string.Empty, // Set an empty phone number
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(person.PhoneNumber, new ValidationContext(person) { MemberName = nameof(Person.PhoneNumber) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count > 0);
        }

        /// <summary>
        /// Verifies that an invalid phone number in the Person object fails validation.
        /// </summary>
        [TestMethod]
        public void InvalidPhoneNumberShouldFailValidation()
        {
            // Arrange
            var person = new Person
            {
                PhoneNumber = "invalid-phone", // Set an invalid phone number
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(person.PhoneNumber, new ValidationContext(person) { MemberName = nameof(Person.PhoneNumber) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count > 0);
        }

        /// <summary>
        /// Verifies that a long phone number in the Person object fails validation.
        /// </summary>
        [TestMethod]
        public void LongPhoneNumberShouldFailValidation()
        {
            // Arrange
            var person = new Person
            {
                PhoneNumber = new string('1', 21), // Set a phone number exceeding the maximum length
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(person.PhoneNumber, new ValidationContext(person) { MemberName = nameof(Person.PhoneNumber) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count > 0);
        }

        /// <summary>
        /// Verifies that a valid address in the Person object passes validation.
        /// </summary>
        [TestMethod]
        public void ValidAddressShouldPassValidation()
        {
            // Arrange
            var person = new Person
            {
                Address = "123 Main St, City, Country", // Set a valid address
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(person.Address, new ValidationContext(person) { MemberName = nameof(Person.Address) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count == 0);
        }

        /// <summary>
        /// Verifies that a null address in the Person object fails validation.
        /// </summary>
        [TestMethod]
        public void NullAddressShouldFailValidation()
        {
            // Arrange
            var person = new Person
            {
                Address = null, // Set a null address
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(person.Address, new ValidationContext(person) { MemberName = nameof(Person.Address) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count > 0);
        }

        /// <summary>
        /// Verifies that an empty address in the Person object fails validation.
        /// </summary>
        [TestMethod]
        public void EmptyAddressShouldFailValidation()
        {
            // Arrange
            var person = new Person
            {
                Address = string.Empty, // Set an empty address
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(person.Address, new ValidationContext(person) { MemberName = nameof(Person.Address) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count > 0);
        }

        /// <summary>
        /// Verifies that a short address in the Person object fails validation.
        /// </summary>
        [TestMethod]
        public void ShortAddressShouldFailValidation()
        {
            // Arrange
            var person = new Person
            {
                Address = "Short", // Set a short address
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(person.Address, new ValidationContext(person) { MemberName = nameof(Person.Address) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count > 0);
        }

        /// <summary>
        /// Verifies that a long address in the Person object fails validation.
        /// </summary>
        [TestMethod]
        public void LongAddressShouldFailValidation()
        {
            // Arrange
            var person = new Person
            {
                Address = new string('A', 201), // Set an address exceeding the maximum length
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(person.Address, new ValidationContext(person) { MemberName = nameof(Person.Address) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count > 0);
        }

        /// <summary>
        /// Verifies that the Type property of the Person class must be a valid BookType enum value.
        /// </summary>
        [TestMethod]
        public void TypeShouldBeReader()
        {
            // Arrange
            var person = new Person();

            // Act
            person.Type = PersonType.Reader; // Set a valid enum value

            // Assert
            var validationContext = new ValidationContext(person) { MemberName = nameof(Person.Type) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(person.Type, validationContext, validationResults);

            Assert.IsTrue(isValid);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Verifies that the Type property of the Person class must be a valid BookType enum value.
        /// </summary>
        [TestMethod]
        public void TypeShouldBeLibraryPersonnel()
        {
            // Arrange
            var person = new Person();

            // Act
            person.Type = PersonType.LibraryPersonnel; // Set a valid enum value

            // Assert
            var validationContext = new ValidationContext(person) { MemberName = nameof(Person.Type) };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(person.Type, validationContext, validationResults);

            Assert.IsTrue(isValid);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Verifies that setting different enum values format for the Type property results in equality.
        /// </summary>
        [TestMethod]
        public void DifferentEnumValuesFormatShouldBeEqual()
        {
            // Arrange
            var personA = new Person();
            var personB = new Person();

            // Act
            personA.Type = PersonType.Reader;
            personB.Type = 0;

            // Assert
            Assert.IsTrue(personA.Type == personB.Type);
        }

        /// <summary>
        /// Verifies that a Person object with both email address and phone number passes validation.
        /// </summary>
        [TestMethod]
        public void BothEmailAndPhoneNumberProvidedShouldPassValidation()
        {
            // Arrange
            var person = new Person
            {
                EmailAddress = "john.doe@example.com", // Set a valid email address
                PhoneNumber = "123456789", // Set a valid phone number
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(person.IsEmailOrPhoneNumberProvided, new ValidationContext(person) { MemberName = nameof(Person.IsEmailOrPhoneNumberProvided) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count == 0);
        }

        /// <summary>
        /// Verifies that a Person object with neither email address nor phone number fails validation.
        /// </summary>
        [TestMethod]
        public void NeitherEmailNorPhoneNumberProvidedShouldFailValidation()
        {
            // Arrange
            var person = new Person();

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(person.IsEmailOrPhoneNumberProvided, new ValidationContext(person) { MemberName = nameof(Person.IsEmailOrPhoneNumberProvided) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count > 0);
        }

        /// <summary>
        /// Verifies that a Person object with null email address but a valid phone number passes validation.
        /// </summary>
        [TestMethod]
        public void NullEmailAddressButValidPhoneNumberShouldPassValidation()
        {
            // Arrange
            var person = new Person
            {
                PhoneNumber = "123456789", // Set a valid phone number
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(person.IsEmailOrPhoneNumberProvided, new ValidationContext(person) { MemberName = nameof(Person.IsEmailOrPhoneNumberProvided) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count == 0);
        }

        /// <summary>
        /// Verifies that a Person object with a valid email address but null phone number passes validation.
        /// </summary>
        [TestMethod]
        public void ValidEmailAddressButNullPhoneNumberShouldPassValidation()
        {
            // Arrange
            var person = new Person
            {
                EmailAddress = "john.doe@example.com", // Set a valid email address
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(person.IsEmailOrPhoneNumberProvided, new ValidationContext(person) { MemberName = nameof(Person.IsEmailOrPhoneNumberProvided) }, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count == 0);
        }

        /// <summary>
        /// Verifies that a Person object with a valid address passes validation.
        /// </summary>
        [TestMethod]
        public void ValidPersonShouldPassValidation()
        {
            // Arrange
            var person = new Person
            {
                Id = 1,
                CNP = "1234567890123",
                FirstName = "John",
                LastName = "Doe",
                Address = "123 Main St, City, Country", // Set a valid address
                Type = PersonType.Reader,
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(person, new ValidationContext(person), validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count == 0);
        }

        /// <summary>
        /// Verifies that a Person object with a null address fails validation.
        /// </summary>
        [TestMethod]
        public void NullAddressPersonShouldFailValidation()
        {
            // Arrange
            var person = new Person
            {
                Id = 1,
                CNP = "1234567890123",
                FirstName = "John",
                LastName = "Doe",
                Address = null, // Set a null address
                Type = PersonType.Reader,
            };

            // Act
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(person, new ValidationContext(person), validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count > 0);
        }
    }
}
