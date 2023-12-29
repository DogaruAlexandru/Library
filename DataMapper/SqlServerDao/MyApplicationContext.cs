using DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

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
        public virtual DbSet<Borrowed> Borroweds { get; set; }
        public virtual DbSet<Edition> Editions { get; set; }
        public virtual DbSet<People> Peoples { get; set; }

    }
}
