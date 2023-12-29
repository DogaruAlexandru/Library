using DataMapper.SqlServerDao;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataMapper
{
    internal class SQLPersonDataService : IPersonDataService
    {
        public void AddPerson(Person person)
        {
            using (var context = new MyApplicationContext())
            {
                context.Persons.Add(person);
                context.SaveChanges();
            }
        }

        public void DeletePerson(Person person)
        {
            using (var context = new MyApplicationContext())
            {
                context.Entry(person).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public IList<Person> GetAllPersons()
        {
            using (var context = new MyApplicationContext())
            {
                return context.Persons.ToList();
            }
        }

        public Person GetPersonById(int id)
        {
            using (var context = new MyApplicationContext())
            {
                return context.Persons.Find(id);
            }
        }

        public void UpdatePerson(Person person)
        {
            using (var context = new MyApplicationContext())
            {
                context.Entry(person).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
