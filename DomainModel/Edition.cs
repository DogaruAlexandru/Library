// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Edition.cs" company="Transilvania University of Brasov">
//   Copyright (c) Dogaru Alexandru.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents different types of books.
    /// </summary>
    public enum BookType
    {
        /// <summary>
        /// Trade hardcover book type.
        /// </summary>
        TradeHardcover,

        /// <summary>
        /// Mass market hardcover book type.
        /// </summary>
        MassMarketHardcover,

        /// <summary>
        /// Oversized hardcover book type.
        /// </summary>
        OversizedHardcover,

        /// <summary>
        /// Library binding book type.
        /// </summary>
        LibraryBinding,

        /// <summary>
        /// Deluxe editions book type.
        /// </summary>
        DeluxeEditions,

        /// <summary>
        /// Collector edition book type.
        /// </summary>
        CollectorEdition,

        /// <summary>
        /// Case wrap hardcover book type.
        /// </summary>
        CaseWrapHardcover,

        /// <summary>
        /// Dust jacket book type.
        /// </summary>
        DustJacket
    }

    /// <summary>
    /// Represents an edition of a book in the domain model.
    /// </summary>
    public partial class Edition
    {
        /// <summary>
        /// Gets or sets the unique identifier of the edition.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the type of the edition.
        /// </summary>
        [Required(ErrorMessage = "The Type cannot be null")]
        [EnumDataType(typeof(BookType))]
        public BookType Type { get; set; }

        /// <summary>
        /// Gets or sets the page count of the edition.
        /// </summary>
        [Required(ErrorMessage = "The PageCount cannot be null")]
        public uint PageCount { get; set; }

        /// <summary>
        /// Gets or sets the name of the edition.
        /// </summary>
        [Required(ErrorMessage = "The Name cannot be null")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The length must be between 1 and 100")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the publisher of the edition.
        /// </summary>
        [Required(ErrorMessage = "The Publisher cannot be null")]
        [StringLength(100, MinimumLength = 1)]
        public string Publisher { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of copies that cannot be borrowed for the edition.
        /// </summary>
        [Required(ErrorMessage = "The CanNotBorrow cannot be null")]
        public uint CanNotBorrow { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of copies that can be borrowed for the edition.
        /// </summary>
        [Required(ErrorMessage = "The CanBorrow cannot be null")]
        public uint CanBorrow { get; set; }

        /// <summary>
        /// Gets or sets the book associated with the edition.
        /// </summary>
        [Required(ErrorMessage = "The Book cannot be null")]
        public Book Book { get; set; }
    }
}
