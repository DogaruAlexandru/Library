// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Author.cs" company="Transilvania University of Brasov">
//   Copyright (c) Dogaru Alexandru.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents an author in the domain model.
    /// </summary>
    public partial class Author
    {
        /// <summary>
        /// Gets or sets the unique identifier of the author.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the author.
        /// </summary>
        [Required(ErrorMessage = "The Name cannot be null")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The length must be between 1 and 100")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the collection of books written by the author.
        /// </summary>
        [Required(ErrorMessage = "The Books collection cannot be null")]
        public virtual ICollection<Book> Books { get; set; }
    }
}
