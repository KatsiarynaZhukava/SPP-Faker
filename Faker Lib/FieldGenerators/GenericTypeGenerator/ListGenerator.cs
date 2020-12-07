using System;
using System.Collections;
using System.Collections.Generic;

namespace Faker_Lib.FieldGenerators.GenericTypeGenerator
{
    class ListGenerator : IGenericTypeGenerator
    {
        protected IDictionary<Type, ISimpleTypeGenerator> simpleTypeGenerators;
        public Type GeneratedType { get; protected set; }

        public object Generate(Type baseType)
        {
            IList result = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(baseType));
            int listSize = new Random().Next() % 20;

            if (simpleTypeGenerators.TryGetValue(baseType, out ISimpleTypeGenerator baseTypeGenerator))
            {
                for (int i = 0; i < listSize; i++)
                {
                    result.Add(baseTypeGenerator.Generate());
                }
            }
            /*else
            {
                if (baseType.IsArray)
                {
                    byte listSize = (byte)byteValueGenerator.Generate();

                    for (int i = 0; i < listSize; i++)
                    {
                        result.Add(Generate(baseType.GetElementType()));
                    }
                }
                else
                {
                    byte listSize = (byte)byteValueGenerator.Generate();
                    for (int i = 0; i < listSize; i++)

                    {

                        result.Add(Generate(baseType.GetGenericArguments()[0]));
                    }
                }
            }*/
            return result;
        }

        public ListGenerator(IDictionary<Type, ISimpleTypeGenerator> simpleTypeGenerators)
        {
            GeneratedType = typeof(List<>);
            this.simpleTypeGenerators = simpleTypeGenerators;
        }
    }
}
