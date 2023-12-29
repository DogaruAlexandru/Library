using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public enum BookType
    {
        TradeHardcover,
        MassMarketHardcover,
        OversizedHardcover,
        LibraryBinding,
        DeluxeEditions,
        CollectorEdition,
        CaseWrapHardcover,
        DustJacket
    }
    public partial class Edition
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Type cannot be null")]
        [EnumDataType(typeof(BookType))]
        public BookType Type { get; set; }

        [Required(ErrorMessage = "The PageCount cannot be null")]
        public uint PageCount { get; set; }

        [Required(ErrorMessage = "The Name cannot be null")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The length must be between 1 and 100")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Publisher cannot be null")]
        [StringLength(100, MinimumLength = 1)]
        public string Publisher { get; set; }

        [Required(ErrorMessage = "The CanNotBorrow cannot be null")]
        public uint CanNotBorrow { get; set; }

        [Required(ErrorMessage = "The CanBorrow cannot be null")]
        public uint CanBorrow { get; set; }

    }
}
