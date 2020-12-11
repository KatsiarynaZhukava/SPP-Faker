using System;

namespace Faker_Lib.FieldGenerators
{
    class ByteGenerator : ISimpleTypeGenerator
    {
        private Random random = new Random();
        private Type generatedType = typeof(byte);
        public Type GeneratedType { get => generatedType; }

        public object Generate()
        {
            return (byte)random.Next();
        }
    }
}
