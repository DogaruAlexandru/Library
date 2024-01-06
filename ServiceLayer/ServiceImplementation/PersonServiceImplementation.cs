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
    public class PersonServiceImplementation : BaseService, IPersonService
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(BookDomainServicesImplementation));

        public void AddPerson(Person person)
        {
            ValidateEntity(person);

            log.Info($"Adding Person with ID: {person.Id}");

            DAOFactoryMethod.CurrentDAOFactory.PersonDataService.AddPerson(person);
        }

        public void DeletePerson(Person person)
        {
            log.Debug($"Deleting Person with ID: {person.Id}");

            DAOFactoryMethod.CurrentDAOFactory.PersonDataService.DeletePerson(person);
        }

        public IList<Person> GetAllPersons()
        {
            log.Debug("Getting all Persons.");

            return DAOFactoryMethod.CurrentDAOFactory.PersonDataService.GetAllPersons();
        }

        public Person GetPersonById(int id)
        {
            log.Debug($"Getting Person with ID: {id}");

            return DAOFactoryMethod.CurrentDAOFactory.PersonDataService.GetPersonById(id);
        }

        public void UpdatePerson(Person person)
        {
            ValidateEntity(person);

            log.Info($"Updating Person with ID: {person.Id}");

            DAOFactoryMethod.CurrentDAOFactory.PersonDataService.UpdatePerson(person);
        }
    }
}
