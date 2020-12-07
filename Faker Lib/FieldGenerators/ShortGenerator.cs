using System;

namespace Faker_Lib.FieldGenerators
{
    class ShortGenerator : ISimpleTypeGenerator
    {
        private Random random = new Random();
        public Type generatedType = typeof(short);
        public Type GeneratedType { get => generatedType; }

        public object Generate()
        {
            return (short)random.Next();
        }
    }
}
