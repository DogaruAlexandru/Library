using DataMapper;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceImplementation
{
    public class PersonServiceImplementation : BaseService, IPersonService
    {
        public void AddPerson(Person person)
        {
            ValidateEntity(person);
            DAOFactoryMethod.CurrentDAOFactory.PersonDataService.AddPerson(person);
        }

        public void DeletePerson(Person person)
        {
            DAOFactoryMethod.CurrentDAOFactory.PersonDataService.DeletePerson(person);
        }

        public IList<Person> GetAllPersons()
        {
            return DAOFactoryMethod.CurrentDAOFactory.PersonDataService.GetAllPersons();
        }

        public Person GetPersonById(int id)
        {
            return DAOFactoryMethod.CurrentDAOFactory.PersonDataService.GetPersonById(id);
        }

        public void UpdatePerson(Person person)
        {
            ValidateEntity(person);
            DAOFactoryMethod.CurrentDAOFactory.PersonDataService.UpdatePerson(person);
        }
    }
}
