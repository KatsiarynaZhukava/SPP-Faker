using System;

namespace Faker_Lib.FieldGenerators
{
    class SByteGenerator : IGenerator
    {
        private Random random = new Random();
        public Type generatedType { get; private set; }

        public object Generate()
        {
            return (sbyte)random.Next();
        }
    }
}
