using DataMapper;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceImplementation
{
    public class EditionServiceImplementation : BaseService, IEditionService
    {
        public void AddEdition(Edition edition)
        {
            ValidateEntity(edition);
            DAOFactoryMethod.CurrentDAOFactory.EditionDataService.AddEdition(edition);
        }

        public void DeleteEdition(Edition edition)
        {
            DAOFactoryMethod.CurrentDAOFactory.EditionDataService.DeleteEdition(edition);
        }

        public IList<Edition> GetAllEditions()
        {
            return DAOFactoryMethod.CurrentDAOFactory.EditionDataService.GetAllEditions();
        }

        public Edition GetEditionById(int id)
        {
            return DAOFactoryMethod.CurrentDAOFactory.EditionDataService.GetEditionById(id);
        }

        public void UpdateEdition(Edition edition)
        {
            ValidateEntity(edition);
            DAOFactoryMethod.CurrentDAOFactory.EditionDataService.UpdateEdition(edition);
        }
    }
}
