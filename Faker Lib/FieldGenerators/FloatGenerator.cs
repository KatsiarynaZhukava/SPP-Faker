using System;

namespace Faker_Lib.FieldGenerators
{
    class FloatGenerator : ISimpleTypeGenerator
    {
        private Random random = new Random();
        public Type generatedType = typeof(float);
        public Type GeneratedType { get => generatedType; }
        public object Generate()
        {
            return (float)random.NextDouble();
        }
    }
}
