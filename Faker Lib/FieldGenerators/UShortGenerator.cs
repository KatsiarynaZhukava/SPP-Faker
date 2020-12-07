using System;
using System.Collections.Generic;
using System.Text;

namespace Faker_Lib.FieldGenerators
{
    class UShortGenerator : ISimpleTypeGenerator
    {
        private Random random = new Random();
        public Type generatedType = typeof(ushort);
        public Type GeneratedType { get => generatedType; }

        public object Generate()
        {
            return (ushort)random.Next();
        }
    }
}
