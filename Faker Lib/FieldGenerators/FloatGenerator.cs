using System;

namespace Faker_Lib.FieldGenerators
{
    class FloatGenerator : IGenerator
    {
        private Random random = new Random();

        public object Generate()
        {
            return (float)random.NextDouble();
        }
    }
}
