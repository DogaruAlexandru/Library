using DataMapper;
using DataMapper.SqlServerDao;
using DomainModel;
using ServiceLayer;
using ServiceLayer.ServiceImplementation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    internal class MyConsoleApplication
    {
        static void Main(string[] args)
        {
            var context = new MyApplicationContext();

            //IAuthorService service = new AuthorServicesImplementation();

            //foreach (var item in service.GetAllAuthors())
            //{
            //    Console.WriteLine(item.Name);
            //}

            //IBookDomainService service1 = new BookDomainServicesImplementation();

            //foreach (var item in service1.GetAllBookDomains())
            //{
            //    Console.WriteLine(item.Name);
            //}

            //Author myAuthor = new Author
            //{
            //    Name = "Jane Doe",
            //    Books = new List<Book>()

            //};

            //IAuthorService service = new AuthorServicesImplementation();
            //service.AddAuthor(myAuthor);

            //IBookDomainService service = new BookDomainServicesImplementation();
            //BookDomain bookDomain = service.GetBookDomainById(1);
            //Console.WriteLine(bookDomain);
        }
    }
}
