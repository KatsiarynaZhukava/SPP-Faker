using System;
using System.Collections.Generic;
using System.Text;
using FakerLib;

namespace Faker_Lib.FieldGenerators.GenericTypeGenerator
{
    class ListGenerator : IGenericTypeGenerator
    {
        private Random random = new Random();

        public Type generatedType { get; private set; }
        public Faker faker { get; private set; }
        public ListGenerator(Random rand)
        {
            random = rand;
            generatedType = typeof(List<>);
            faker = new Faker();
        }

        public object Generate(Type type)
        {
            List<object> list = new List<object>();

            int length = random.Next(5, 10);

            for (int i = 0; i < length; i++)
            {
                list.Add(faker.Create(type));
            }
            return list;
        }
    }
}
