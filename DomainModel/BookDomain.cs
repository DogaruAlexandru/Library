using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public partial class BookDomain
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public BookDomain ParentDomain { get; set; }

        public virtual HashSet<Book> Books { get; set; }
    }
}
