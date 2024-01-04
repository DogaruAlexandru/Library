using DataMapper;
using DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.ServiceImplementation
{
    public class AuthorServicesImplementation : BaseService, IAuthorService
    {
        public void AddAuthor(Author author)
        {
            ValidateEntity(author);
            DAOFactoryMethod.CurrentDAOFactory.AuthorDataService.AddAuthor(author);
        }

        public void DeleteAuthor(Author author)
        {
            DAOFactoryMethod.CurrentDAOFactory.AuthorDataService.DeleteAuthor(author);
        }

        public IList<Author> GetAllAuthors()
        {
            return DAOFactoryMethod.CurrentDAOFactory.AuthorDataService.GetAllAuthors();
        }

        public Author GetAuthorById(int id)
        {
            return DAOFactoryMethod.CurrentDAOFactory.AuthorDataService.GetAuthorById(id);
        }

        public void UpdateAuthor(Author author)
        {
            ValidateEntity(author);
            DAOFactoryMethod.CurrentDAOFactory.AuthorDataService.UpdateAuthor(author);
        }
    }
}
