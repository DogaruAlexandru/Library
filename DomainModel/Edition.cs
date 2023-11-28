using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public enum EditionType
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

        public EditionType Type { get; set; }

        public int PageCount { get; set; }

        public string Name { get; set; }
        
        public uint Total { get; set; }

        public uint Borrow { get; set; }

    }
}
