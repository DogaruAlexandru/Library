// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Book.cs" company="Transilvania University of Brasov">
//   Copyright (c) Dogaru Alexandru.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents a book in the domain model.
    /// </summary>
    public partial class Book
    {
        /// <summary>
        /// Gets or sets the unique identifier of the book.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the book.
        /// </summary>
        [Required(ErrorMessage = "The Title cannot be null")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The length must be between 1 and 100")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description of the book.
        /// </summary>
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The length must be between 1 and 100")]
        public string Description { get; set; }

        public virtual ICollection<Edition> Editions { get; set; }

        /// <summary>
        /// Gets or sets the collection of domains associated with the book.
        /// </summary>
        [Required(ErrorMessage = "The BookDomains collection cannot be null")]
        [MinLength(1, ErrorMessage = "At least one BookDomain is required")]
        public virtual ICollection<BookDomain> BookDomains { get; set; }

        public virtual ICollection<Author> Authors { get; set; }
    }
}
