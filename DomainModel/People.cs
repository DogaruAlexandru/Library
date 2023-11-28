using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public enum PeopleType
    {
        Reader,
        LibraryPersonnel
    }

    public partial class People
    {
        public int Id { get; set; }

        public string SocialId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public PeopleType Type { get; set; }

    }
}
