using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public partial class BorrowedBook
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Reader cannot be null")]
        [ForeignKey("Reader")]
        public int ReaderId { get; set; }

        [Required(ErrorMessage = "The Staff cannot be null")]
        [ForeignKey("Staff")]
        public int StaffId { get; set; }

        [Required(ErrorMessage = "The Reader cannot be null")]
        [ForeignKey("ReaderId")]
        public Person Reader { get; set; }

        [Required(ErrorMessage = "The Staff cannot be null")]
        [ForeignKey("StaffId")]
        public Person Staff { get; set; }

        [Required(ErrorMessage = "The Edition cannot be null")]
        public Edition Edition { get; set; }

        [Required(ErrorMessage = "The BorrowDate cannot be null")]
        public DateTime BorrowDate { get; set; }

        [Required(ErrorMessage = "The DueDate cannot be null")]
        public DateTime DueDate { get; set; }

        public DateTime ReturnedDate { get; set; }
    }
}
