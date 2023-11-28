using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public partial class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }
        
        public string Description { get; set; }

        public ICollection<Edition> Editions { get; set; }

        public virtual ICollection<BookDomain> BookDomains { get; set; }

        public virtual ICollection<Author> Authors { get; set; }

    }
}
