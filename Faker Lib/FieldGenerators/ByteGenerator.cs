using System;

namespace Faker_Lib.FieldGenerators
{
    class ByteGenerator : IGenerator
    {
        private Random random = new Random();
        public Type generatedType { get; private set; }

        public object Generate()
        {
            return (byte)random.Next();
        }
    }
}
