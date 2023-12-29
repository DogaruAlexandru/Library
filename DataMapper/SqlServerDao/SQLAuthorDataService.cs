using DataMapper.SqlServerDao;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataMapper
{
    internal class SQLAuthorDataService : IAuthorDataService
    {
        public void AddAuthor(Author author)
        {
            using (var context = new MyApplicationContext())
            {
                context.Authors.Add(author);
                context.SaveChanges();
            }
        }

        public void DeleteAuthor(Author author)
        {
            using (var context = new MyApplicationContext())
            {
                context.Entry(author).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public IList<Author> GetAllAuthors()
        {
            using (var context = new MyApplicationContext())
            {
                return context.Authors.ToList();
            }
        }

        public Author GetAuthorById(int id)
        {
            using (var context = new MyApplicationContext())
            {
                return context.Authors.Find(id);
            }
        }

        public void UpdateAuthor(Author author)
        {
            using (var context = new MyApplicationContext())
            {
                context.Entry(author).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
