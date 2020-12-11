using System;
using System.Collections;
using System.Collections.Generic;

namespace Faker_Lib.FieldGenerators.GenericTypeGenerator
{
    class ListGenerator : IGenericTypeGenerator
    {
        protected IDictionary<Type, ISimpleTypeGenerator> simpleTypeGenerators;
        public Type GeneratedType { get; protected set; }

        public ListGenerator(IDictionary<Type, ISimpleTypeGenerator> simpleTypeGenerators)
        {
            GeneratedType = typeof(List<>);
            this.simpleTypeGenerators = simpleTypeGenerators;
        }

        public object Generate(Type type, Faker faker)
        {
            IList result = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(type));
            int listSize = new Random().Next() % 20;

            if (simpleTypeGenerators.TryGetValue(type, out ISimpleTypeGenerator simpleTypeGenerator))
            {
                for (int i = 0; i < listSize; i++)
                {
                    result.Add(simpleTypeGenerator.Generate());
                }
            }
            else if (type.IsGenericType)
            {
                for (int i = 0; i < listSize; i++)
                {
                    result.Add(Generate(type.GetGenericArguments()[0], faker));
                }
            }
            else if (type.IsClass && !type.IsAbstract && !type.IsInterface)
            {
                for (int i = 0; i < listSize; i++)
                {                    
                    //result.Add(Activator.CreateInstance(type));
                    result.Add(faker.Generate(type));
                }
            }
            else
            {
                result = null;
            }
            return result;
        }
    }
}
