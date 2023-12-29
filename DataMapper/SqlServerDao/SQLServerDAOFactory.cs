using DataMapper.SqlServerDao;

namespace DataMapper
{
    class SQLServerDAOFactory : IDAOFactory
    {
        public IAuthorDataService AuthorDataService
        {
            get
            {
                return new SQLAuthorDataService();
            }
        }

        public IBookDataService BookDataService
        {
            get
            {
                return new SQLBookDataService();
            }
        }

        public IBookDomainDataService BookDomainDataService
        {
            get
            {
                return new SQLBookDomainDataService();
            }
        }

        public IBorrowedBookDataService BorrowedBookDataService
        {
            get
            {
                return new SQLBorrowedBookDataService();
            }
        }

        public IEditionDataService EditionDataService
        {
            get
            {
                return new SQLEditionDataService();
            }
        }

        public IPersonDataService PersonDataService
        {
            get
            {
                return new SQLPersonDataService();
            }
        }
    }
}