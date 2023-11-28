using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public partial class Borrowed
    {
        public int Id { get; set; }

        public People Reader { get; set; }

        public Edition Edition { get; set; }

        public DateTime BorrowDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime ReturnedDate { get; set; }


    }
}
