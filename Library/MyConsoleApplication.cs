using DataMapper;
using DataMapper.SqlServerDao;
using DomainModel;
using log4net;
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
        //private static readonly ILog log = LogManager.GetLogger(typeof(MyConsoleApplication));
        static void Main(string[] args)
        {
            var context = new MyApplicationContext();

            //BookDomain bookInstance = new BookDomain
            //{
            //    Name = "Stiinta",
            //    ParentDomain = null,  // You may set the ParentDomain if needed
            //    Books = new List<Book>()  // Initialize the Books collection
            //};

            //IBookDomainService service = new BookDomainServicesImplementation();
            //service.AddBookDomain(bookInstance);

            //log.Info("Button pressed.");
            //log.Info("Button pressed.");

        }
    }
}
