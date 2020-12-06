using System;

namespace Faker_Lib.FieldGenerators
{
    class CharGenerator : IGenerator
    {
        private Random random = new Random();
        public Type generatedType { get; private set; }

        public object Generate()
        {
            return (char) random.Next(0xFF);
        }
    }
}
