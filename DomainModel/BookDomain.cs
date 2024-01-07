// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BookDomain.cs" company="Transilvania University of Brasov">
//   Copyright (c) Dogaru Alexandru.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents a domain associated with books in the domain model.
    /// </summary>
    public partial class BookDomain
    {
        /// <summary>
        /// Gets or sets the unique identifier of the book domain.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the book domain.
        /// </summary>
        [Required(ErrorMessage = "The Name cannot be null")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The length must be between 1 and 100")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the parent domain associated with the book domain.
        /// </summary>
        public BookDomain ParentDomain { get; set; }

        /// <summary>
        /// Gets or sets the collection of books associated with the book domain.
        /// </summary>
        [Required(ErrorMessage = "The Books collection cannot be null")]
        public virtual ICollection<Book> Books { get; set; }
    }
}
