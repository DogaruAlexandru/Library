// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBorrowedBookDataService.cs" company="Transilvania University of Brasov">
//   Copyright (c) Dogaru Alexandru.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DataMapper
{
    using System;
    using System.Collections.Generic;
    using DomainModel;

    /// <summary>
    /// Interface for accessing and manipulating data related to borrowed books.
    /// </summary>
    public interface IBorrowedBookDataService
    {
        /// <summary>
        /// Gets a list of all borrowed books.
        /// </summary>
        /// <returns>The list of all borrowed books.</returns>
        IList<BorrowedBook> GetAllBorrowedBooks();

        /// <summary>
        /// Gets a borrowed book by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the borrowed book.</param>
        /// <returns>The borrowed book with the specified identifier, or null if not found.</returns>
        BorrowedBook GetBorrowedBookById(int id);

        /// <summary>
        /// Adds a new borrowed book to the data store.
        /// </summary>
        /// <param name="borrowedBook">The borrowed book to be added.</param>
        void AddBorrowedBook(BorrowedBook borrowedBook);

        /// <summary>
        /// Deletes a borrowed book from the data store.
        /// </summary>
        /// <param name="borrowedBook">The borrowed book to be deleted.</param>
        void DeleteBorrowedBook(BorrowedBook borrowedBook);

        /// <summary>
        /// Updates the information of an existing borrowed book in the data store.
        /// </summary>
        /// <param name="borrowedBook">The borrowed book with updated information.</param>
        void UpdateBorrowedBook(BorrowedBook borrowedBook);

        /// <summary>
        /// Counts the number of borrowed books for a specific edition with a null returned date.
        /// </summary>
        /// <param name="edition">The edition for which to count the borrowed books.</param>
        /// <returns>The number of borrowed books for the specified edition with a null returned date.</returns>
        int CountBorrowedBooksByEditionWithNullReturnedDate(Edition edition);

        /// <summary>
        /// Counts the number of borrowed books for a specific person on a given date.
        /// </summary>
        /// <param name="person">The person for whom to count the borrowed books.</param>
        /// <param name="date">The date on which to count the borrowed books.</param>
        /// <returns>The number of borrowed books for the specified person on the given date.</returns>
        int CountBorrowedBooksByPersonAndDate(Person person, DateTime date);

        /// <summary>
        /// Counts the number of borrowed books for a specific person and book domain after a certain date.
        /// </summary>
        /// <param name="person">The person for whom to count the borrowed books.</param>
        /// <param name="bookDomain">The book domain for which to count the borrowed books.</param>
        /// <param name="date">The date after which to count the borrowed books.</param>
        /// <returns>The number of borrowed books for the specified person and book domain after the given date.</returns>
        int CountBorrowedBooksForPersonAndDomainAfterDate(Person person, BookDomain bookDomain, DateTime date);

        /// <summary>
        /// Gets the differences in due dates for borrowed books for a specific person after a certain date.
        /// </summary>
        /// <param name="person">The person for whom to get the differences in due dates.</param>
        /// <param name="date">The date after which to get the differences in due dates.</param>
        /// <returns>The list of differences in due dates for borrowed books for the specified person after the given date.</returns>
        List<int> GetDueDateDifferencesForPersonAfterDate(Person person, DateTime date);

        /// <summary>
        /// Counts the number of borrowed books for a specific edition and person after a certain date.
        /// </summary>
        /// <param name="person">The person for whom to count the borrowed books.</param>
        /// <param name="edition">The edition for which to count the borrowed books.</param>
        /// <param name="date">The date after which to count the borrowed books.</param>
        /// <returns>The number of borrowed books for the specified edition and person after the given date.</returns>
        int CountBorrowedBooksByEditionForPersonAfterDate(Person person, Edition edition, DateTime date);

        /// <summary>
        /// Counts the number of books borrowed by a specific person on a given date.
        /// </summary>
        /// <param name="person">The person for whom to count the borrowed books.</param>
        /// <param name="date">The date on which to count the borrowed books.</param>
        /// <returns>The number of books borrowed by the specified person on the given date.</returns>
        int CountBooksBorrowedByPersonOnDate(Person person, DateTime date);

        /// <summary>
        /// Counts the number of books borrowed by a specific staff member on a given date.
        /// </summary>
        /// <param name="person">The staff member for whom to count the borrowed books.</param>
        /// <param name="date">The date on which to count the borrowed books.</param>
        /// <returns>The number of books borrowed by the specified staff member on the given date.</returns>
        int CountBooksBorrowedBySuffOnDate(Person person, DateTime date);
    }
}
