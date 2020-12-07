using Faker_Lib.FieldGenerators;
using System;

namespace IntGenerator
{
    public class IntGenerator : ISimpleTypeGenerator
    {
        private Random random = new Random();
        public Type generatedType = typeof(int);
        public Type GeneratedType { get => generatedType; }

        public object Generate()
        {
            return random.Next();
        }
    }
}
