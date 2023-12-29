using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper
{
    public interface IAuthorDataService
    {
        IList<Author> GetAllAuthors();

        Author GetAuthorById(int id);

        void AddAuthor(Author Author);

        void DeleteAuthor(Author Author);

        void UpdateAuthor(Author Author);
    }
}
