// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBorrowedBookService.cs" company="Your Company">
//   Copyright (c) Your Company. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ServiceLayer
{
    using System;
    using System.Collections.Generic;
    using DomainModel;

    /// <summary>
    /// Represents the service interface for managing BorrowedBook entities.
    /// </summary>
    /// <remarks>
    /// This interface provides methods for performing CRUD (Create, Read, Update, Delete) operations on BorrowedBook entities.
    /// It also includes additional methods for handling specific business logic related to borrowed books.
    /// </remarks>
    public interface IBorrowedBookService : IValidationService
    {
        /// <summary>
        /// Gets a list of all borrowed books.
        /// </summary>
        /// <returns>List of BorrowedBook entities.</returns>
        IList<BorrowedBook> GetAllBorrowedBooks();

        /// <summary>
        /// Gets a borrowed book by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the borrowed book.</param>
        /// <returns>The BorrowedBook entity.</returns>
        BorrowedBook GetBorrowedBookById(int id);

        /// <summary>
        /// Adds a new borrowed book.
        /// </summary>
        /// <param name="borrowedBook">The BorrowedBook entity to be added.</param>
        void AddBorrowedBook(BorrowedBook borrowedBook);

        /// <summary>
        /// Deletes an existing borrowed book.
        /// </summary>
        /// <param name="borrowedBook">The BorrowedBook entity to be deleted.</param>
        void DeleteBorrowedBook(BorrowedBook borrowedBook);

        /// <summary>
        /// Updates an existing borrowed book.
        /// </summary>
        /// <param name="borrowedBook">The BorrowedBook entity to be updated.</param>
        void UpdateBorrowedBook(BorrowedBook borrowedBook);

        /// <summary>
        /// Checks if a person can borrow more books of a specific edition.
        /// </summary>
        /// <param name="edition">The Edition for which to check if more books can be borrowed.</param>
        /// <returns>True if more books can be borrowed; otherwise, false.</returns>
        bool CanBorrowMoreBooks(Edition edition);

        /// <summary>
        /// Counts the number of books borrowed by a person since a specific date.
        /// </summary>
        /// <param name="person">The Person for whom to count the borrowed books.</param>
        /// <param name="date">The date since which to count the borrowed books.</param>
        /// <returns>The number of borrowed books.</returns>
        int CountBorrowedSinceDateForPerson(Person person, DateTime date);

        /// <summary>
        /// Counts the number of books borrowed by a person within a specific book domain after a certain date.
        /// </summary>
        /// <param name="person">The Person for whom to count the borrowed books.</param>
        /// <param name="bookDomain">The BookDomain for which to count the borrowed books.</param>
        /// <param name="date">The date since which to count the borrowed books.</param>
        /// <returns>The number of borrowed books.</returns>
        int CountBorrowedBooksForPersonAndDomainAfterDate(Person person, BookDomain bookDomain, DateTime date);

        /// <summary>
        /// Gets a list of due date differences for borrowed books by a person after a specific date.
        /// </summary>
        /// <param name="person">The Person for whom to retrieve the due date differences.</param>
        /// <param name="date">The date since which to calculate the due date differences.</param>
        /// <returns>List of due date differences in days.</returns>
        List<int> GetDueDateDifferencesForPersonAfterDate(Person person, DateTime date);

        /// <summary>
        /// Counts the number of books borrowed by a person for a specific edition after a certain date.
        /// </summary>
        /// <param name="person">The Person for whom to count the borrowed books.</param>
        /// <param name="edition">The Edition for which to count the borrowed books.</param>
        /// <param name="date">The date since which to count the borrowed books.</param>
        /// <returns>The number of borrowed books.</returns>
        int CountBorrowedBooksByEditionForPersonAfterDate(Person person, Edition edition, DateTime date);

        /// <summary>
        /// Counts the number of books borrowed by a person on a specific date.
        /// </summary>
        /// <param name="person">The Person for whom to count the borrowed books.</param>
        /// <param name="date">The date on which to count the borrowed books.</param>
        /// <returns>The number of borrowed books.</returns>
        int CountBooksBorrowedByPersonOnDate(Person person, DateTime date);

        /// <summary>
        /// Counts the number of books borrowed by a staff member on a specific date.
        /// </summary>
        /// <param name="person">The Staff member for whom to count the borrowed books.</param>
        /// <param name="date">The date on which to count the borrowed books.</param>
        /// <returns>The number of borrowed books.</returns>
        int CountBooksBorrowedBySuffOnDate(Person person, DateTime date);

        /// <summary>
        /// Borrows multiple books at once.
        /// </summary>
        /// <param name="borrowedBooks">List of BorrowedBook entities to be borrowed.</param>
        void BorrowMultipleBook(List<BorrowedBook> borrowedBooks);
    }
}
