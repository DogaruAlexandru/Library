using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.SqlServerDao
{
    public interface IDAOFactory
    {
        IAuthorDataService AuthorDataService
        {
            get;
        }
        IBookDataService BookDataService
        {
            get;
        }
        IBookDomainDataService BookDomainDataService
        {
            get;
        }
        IBorrowedBookDataService BorrowedBookDataService
        {
            get;
        }
        IEditionDataService EditionDataService
        {
            get;
        }
        IPersonDataService PersonDataService
        {
            get;
        }

    }
}
