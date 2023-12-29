using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public partial class Author
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name cannot be null")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The length must be between 1 and 100")]
        public string Name { get; set; }


        [Required(ErrorMessage = "The Books cannot be null")]
        public virtual ICollection<Book> Books { get; set; }

    }
}
