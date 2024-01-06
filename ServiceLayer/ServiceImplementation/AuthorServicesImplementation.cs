using DataMapper;
using DomainModel;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.ServiceImplementation
{
    public class AuthorServicesImplementation : BaseService, IAuthorService
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(BookDomainServicesImplementation));

        public void AddAuthor(Author author)
        {
            ValidateEntity(author);

            log.Info($"Adding Author with ID: {author.Id}");

            DAOFactoryMethod.CurrentDAOFactory.AuthorDataService.AddAuthor(author);
        }

        public void DeleteAuthor(Author author)
        {
            log.Debug($"Deleting Author with ID: {author.Id}");

            DAOFactoryMethod.CurrentDAOFactory.AuthorDataService.DeleteAuthor(author);
        }

        public IList<Author> GetAllAuthors()
        {
            log.Debug("Getting all Authors.");

            return DAOFactoryMethod.CurrentDAOFactory.AuthorDataService.GetAllAuthors();
        }

        public Author GetAuthorById(int id)
        {
            log.Debug($"Getting Author with ID: {id}");

            return DAOFactoryMethod.CurrentDAOFactory.AuthorDataService.GetAuthorById(id);
        }

        public void UpdateAuthor(Author author)
        {
            ValidateEntity(author);

            log.Info($"Updating Author with ID: {author.Id}");

            DAOFactoryMethod.CurrentDAOFactory.AuthorDataService.UpdateAuthor(author);
        }
    }
}
