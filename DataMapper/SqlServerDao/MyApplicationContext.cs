using DomainModel;
using System.Data.Entity;

namespace DataMapper.SqlServerDao
{
    public class MyApplicationContext : DbContext
    {
        public MyApplicationContext() : base("myConStr")
        {

        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookDomain> BookDomains { get; set; }
        public virtual DbSet<BorrowedBook> BorrowedBooks { get; set; }
        public virtual DbSet<Edition> Editions { get; set; }
        public virtual DbSet<Person> Persons { get; set; }

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
