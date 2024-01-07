// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BorrowedBook.cs" company="Transilvania University of Brasov">
//   Copyright (c) Dogaru Alexandru.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DomainModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Represents a borrowed book in the domain model.
    /// </summary>
    public partial class BorrowedBook
    {
        /// <summary>
        /// Gets or sets the unique identifier of the borrowed book.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the reader's unique identifier associated with the borrowed book.
        /// </summary>
        [Required(ErrorMessage = "The Reader cannot be null")]
        [ForeignKey("Reader")]
        public int ReaderId { get; set; }

        /// <summary>
        /// Gets or sets the staff member's unique identifier associated with the borrowed book.
        /// </summary>
        [Required(ErrorMessage = "The Staff cannot be null")]
        [ForeignKey("Staff")]
        public int StaffId { get; set; }

        /// <summary>
        /// Gets or sets the reader associated with the borrowed book.
        /// </summary>
        [Required(ErrorMessage = "The Reader cannot be null")]
        [ForeignKey("ReaderId")]
        public Person Reader { get; set; }

        /// <summary>
        /// Gets or sets the staff member associated with the borrowed book.
        /// </summary>
        [Required(ErrorMessage = "The Staff cannot be null")]
        [ForeignKey("StaffId")]
        public Person Staff { get; set; }

        /// <summary>
        /// Gets or sets the edition of the book being borrowed.
        /// </summary>
        [Required(ErrorMessage = "The Edition cannot be null")]
        public Edition Edition { get; set; }

        /// <summary>
        /// Gets or sets the date when the book was borrowed.
        /// </summary>
        [Required(ErrorMessage = "The BorrowDate cannot be null")]
        public DateTime BorrowDate { get; set; }

        /// <summary>
        /// Gets or sets the due date for returning the borrowed book.
        /// </summary>
        [Required(ErrorMessage = "The DueDate cannot be null")]
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Gets or sets the date when the borrowed book was returned.
        /// </summary>
        public DateTime? ReturnedDate { get; set; }
    }
}
