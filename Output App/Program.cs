﻿using System;
using FakerLib;
using Output_App.TestClasses;

namespace Output_App
{
    class Program
    {
        static void Main(string[] args)
        {
            var faker = new Faker();
            Library library = faker.Create<Library>();
            Book book = faker.Create<Book>();
        }
    }
}