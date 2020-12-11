using Faker_Lib.FieldGenerators;
using System;

namespace CharGenerator
{
    public class CharGenerator : ISimpleTypeGenerator
    {
        private Random random = new Random();
        public Type generatedType = typeof(char);
        public Type GeneratedType { get => generatedType; }

        public object Generate()
        {
            return (char)random.Next('a', 'z');
        }
    }
}
