using System;
using Faker_Lib.FieldGenerators.CustomGenerators;
using Output_App.TestClasses;
using Output_App.Serializer;
using Faker_Lib;

namespace Output_App
{
    class Program
    {
        static void Main(string[] args)
        {
            ISerializer jsonSerializer = new JsonSerializer();

            FakerConfig fakerConfig = new FakerConfig();
            fakerConfig.Add<Book, string, CityGenerator>(bk => bk.CityOfPublication);
            Faker faker = new Faker(fakerConfig);



            Book book = faker.Generate<Book>();
            Library library = faker.Generate<Library>();
            jsonSerializer.Serialize(book);
            jsonSerializer.Serialize(library);

            Console.ReadKey();
        }
    }
}
