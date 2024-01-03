using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public partial class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Title cannot be null")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The length must be between 1 and 100")]
        public string Title { get; set; }
        
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The length must be between 1 and 100")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The Editions cannot be null")]
        public virtual ICollection<Edition> Editions { get; set; }

        [Required(ErrorMessage = "The BookDomain cannot be null")]
        [MinLength(1, ErrorMessage = "At least one BookDomain is required")]
        public virtual ICollection<BookDomain> BookDomains { get; set; }

        [Required(ErrorMessage = "The Authors cannot be null")]
        public virtual ICollection<Author> Authors { get; set; }

    }
}
