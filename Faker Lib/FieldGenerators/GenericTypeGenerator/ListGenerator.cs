using System;
using System.Collections.Generic;
using System.Text;

namespace Faker_Lib.FieldGenerators.GenericTypeGenerator
{
    class ListGenerator : IGenericTypeGenerator
    {
        public object Generate(Type type)
        {
            List<object> list = new List<object>();
            const int MinLength = 3;
            const int MaxLength = 10;
            int length = random.Next(MinLength, MaxLength);


            for (int i = 0; i < length; i++)
            {
                list.Add(faker.Create(type));
            }
            return list;
        }

        public object Generate()
        {
            throw new NotImplementedException();
        }
    }
}
