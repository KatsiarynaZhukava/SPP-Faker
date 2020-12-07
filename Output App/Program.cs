using System;
using FakerLib;
using Output_App.TestClasses;

namespace Output_App
{
    class Program
    {
        static void Main(string[] args)
        {
            Faker faker = new Faker();
            Book book = faker.Create<Book>();
            Library library = faker.Create<Library>();
        }
    }
}
