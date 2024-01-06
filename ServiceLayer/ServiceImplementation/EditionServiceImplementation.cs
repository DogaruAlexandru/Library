using DataMapper;
using DomainModel;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceImplementation
{
    public class EditionServiceImplementation : BaseService, IEditionService
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(BookDomainServicesImplementation));

        public void AddEdition(Edition edition)
        {
            ValidateEntity(edition);

            log.Info($"Adding Edition with ID: {edition.Id}");

            DAOFactoryMethod.CurrentDAOFactory.EditionDataService.AddEdition(edition);
        }

        public void DeleteEdition(Edition edition)
        {
            log.Debug($"Deleting Edition with ID: {edition.Id}");

            DAOFactoryMethod.CurrentDAOFactory.EditionDataService.DeleteEdition(edition);
        }

        public IList<Edition> GetAllEditions()
        {
            log.Debug("Getting all Editions.");

            return DAOFactoryMethod.CurrentDAOFactory.EditionDataService.GetAllEditions();
        }

        public Edition GetEditionById(int id)
        {
            log.Debug($"Getting Edition with ID: {id}");

            return DAOFactoryMethod.CurrentDAOFactory.EditionDataService.GetEditionById(id);
        }

        public void UpdateEdition(Edition edition)
        {
            ValidateEntity(edition);

            log.Info($"Updating Edition with ID: {edition.Id}");

            DAOFactoryMethod.CurrentDAOFactory.EditionDataService.UpdateEdition(edition);
        }
    }
}
