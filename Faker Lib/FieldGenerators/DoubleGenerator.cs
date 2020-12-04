using System;


namespace Faker_Lib.FieldGenerators
{
    class DoubleGenerator : IGenerator
    {
        private Random random = new Random();

        public object Generate()
        {
            return random.NextDouble();
        }
    }
}
