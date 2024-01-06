using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface IAuthorService : IValidationService
    {
        IList<Author> GetAllAuthors();

        Author GetAuthorById(int id);

        void AddAuthor(Author author);

        void DeleteAuthor(Author author);

        void UpdateAuthor(Author author);
    }
}
