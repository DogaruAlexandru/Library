// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MyConsoleApplication.cs" company="Transilvania University of Brasov">
//   Copyright (c) Dogaru Alexandru.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Library
{
    using System;
    using DataMapper.SqlServerDao;
    using DomainModel;
    using ServiceLayer.ServiceImplementation;

    /// <summary>
    /// Represents the main console application.
    /// </summary>
    internal class MyConsoleApplication
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        public static void Main(string[] args)
        {
            var context = new MyApplicationContext();
        }
    }
}
