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
            //var context = new MyApplicationContext();

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
            //    Id = 1,
            //    Name = "Jane Doe",
            //    //Books = new List<Book>()

            //};
            //IAuthorService service = new AuthorServicesImplementation();
            //service.AddAuthor(myAuthor);

            //Person myPerson = new Person
            //{
            //    Id = 1,
            //    CNP = "1234567890123", // Assuming CNP is a 13-digit string
            //    FirstName = "John",
            //    LastName = "Doe",
            //    EmailAddress = "john.doe@example.com",
            //    PhoneNumber = "1234567890",
            //    Address = "123 Main Street",
            //    Type = PersonType.Reader
            //};


            //// Manually trigger validation
            //var validationContext = new ValidationContext(myPerson, serviceProvider: null, items: null);
            //var validationResults = new List<ValidationResult>();
            //bool isValid = Validator.TryValidateObject(myPerson, validationContext, validationResults, validateAllProperties: true);

            //if (!isValid)
            //{
            //    // Validation failed, handle errors
            //    foreach (var validationResult in validationResults)
            //    {
            //        Console.WriteLine(validationResult.ErrorMessage);
            //    }
            //}
            //else
            //{
            //    // Object is valid, proceed with further logic
            //    Console.WriteLine("Object is valid!");
            //}
        }
    }
}
