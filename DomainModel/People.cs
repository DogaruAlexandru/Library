﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public enum PeopleType
    {
        Reader,
        LibraryPersonnel
    }

    public partial class People
    {
        public int Id { get; set; }

        [StringLength(13)]
        public string CNP { get; set; }

        [Required(ErrorMessage = "The FirstName cannot be null")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The length must be between 1 and 100")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The LastName cannot be null")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The length must be between 1 and 100")]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(100, ErrorMessage = "The length must at most 100")]
        public string EmailAddress { get; set; }

        [Phone(ErrorMessage = "Invalid phone number")]
        [StringLength(20, ErrorMessage = "The length must at most 20")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "The Address cannot be null")]
        [StringLength(200, MinimumLength = 10)]
        public string Address { get; set; }

        [Required(ErrorMessage = "The Type cannot be null")]
        [EnumDataType(typeof(PeopleType))]
        public PeopleType Type { get; set; }

        [Required(ErrorMessage = "At least one of EmailAddress or PhoneNumber should not be null")]
        public bool IsEmailOrPhoneNumberProvided => !string.IsNullOrEmpty(EmailAddress) || !string.IsNullOrEmpty(PhoneNumber);

    }
}
