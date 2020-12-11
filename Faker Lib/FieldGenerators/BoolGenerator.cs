using System;

namespace Faker_Lib.FieldGenerators
{
    class BoolGenerator : ISimpleTypeGenerator
    { 
        private Random random = new Random();
        private Type generatedType = typeof(bool);
        public Type GeneratedType { get => generatedType; }

        public object Generate()
        {
            return random.Next() % 2 == 0;
        }
    }
}
