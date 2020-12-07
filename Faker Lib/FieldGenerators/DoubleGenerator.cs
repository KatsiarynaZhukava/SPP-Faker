using System;


namespace Faker_Lib.FieldGenerators
{
    class DoubleGenerator : ISimpleTypeGenerator
    {
        private Random random = new Random();
        public Type generatedType = typeof(Double);
        public Type GeneratedType { get => generatedType; }

        public object Generate()
        {
            return random.NextDouble();
        }
    }
}
