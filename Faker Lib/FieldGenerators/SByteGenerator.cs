using System;

namespace Faker_Lib.FieldGenerators
{
    class SByteGenerator : ISimpleTypeGenerator
    {
        private Random random = new Random();
        public Type generatedType = typeof(sbyte);
        public Type GeneratedType { get => generatedType; }

        public object Generate()
        {
            return (sbyte)random.Next();
        }
    }
}
