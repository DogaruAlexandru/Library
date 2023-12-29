using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper
{
    public interface IPersonDataService
    {
        IList<Person> GetAllPersons();

        Person GetPersonById(int id);

        void AddPerson(Person person);

        void DeletePerson(Person person);

        void UpdatePerson(Person person);
    }
}
