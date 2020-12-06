using System;

namespace Faker_Lib.FieldGenerators
{
    class IntGenerator : IGenerator
    {
        private Random random = new Random();
        public Type generatedType { get; private set; }

        public object Generate()
        {
            return random.Next();
        }
    }
}
