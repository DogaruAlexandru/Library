// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MyApplicationContext.cs" company="Transilvania University of Brasov">
//   Copyright (c) Dogaru Alexandru.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DataMapper.SqlServerDao
{
    using System.Data.Entity;
    using DomainModel;

    /// <summary>
    /// Represents the application context for the Entity Framework, responsible for interacting with the underlying database.
    /// </summary>
    public class MyApplicationContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyApplicationContext"/> class.
        /// </summary>
        public MyApplicationContext() : base("myConStr")
        {
        }

        /// <summary>
        /// Gets or sets the DbSet for the Author entity, representing the corresponding table in the database.
        /// </summary>
        public virtual DbSet<Author> Authors { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the Book entity, representing the corresponding table in the database.
        /// </summary>
        public virtual DbSet<Book> Books { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the BookDomain entity, representing the corresponding table in the database.
        /// </summary>
        public virtual DbSet<BookDomain> BookDomains { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the BorrowedBook entity, representing the corresponding table in the database.
        /// </summary>
        public virtual DbSet<BorrowedBook> BorrowedBooks { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the Edition entity, representing the corresponding table in the database.
        /// </summary>
        public virtual DbSet<Edition> Editions { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the Person entity, representing the corresponding table in the database.
        /// </summary>
        public virtual DbSet<Person> Persons { get; set; }

        /// <summary>
        /// Configures relationships and behaviors for entities in the model.
        /// </summary>
        /// <param name="modelBuilder">The model builder used to configure the database context.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BorrowedBook>()
                .HasRequired(b => b.Reader)
                .WithMany()
                .HasForeignKey(b => b.ReaderId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BorrowedBook>()
                .HasRequired(b => b.Staff)
                .WithMany()
                .HasForeignKey(b => b.StaffId)
                .WillCascadeOnDelete(false);
        }
    }
}
