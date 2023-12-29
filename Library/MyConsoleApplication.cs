using DataMapper.SqlServerDao;
using System;
using System.Collections.Generic;
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
        }
    }
}
