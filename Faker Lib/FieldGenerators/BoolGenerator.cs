using System;

namespace Faker_Lib.FieldGenerators
{
    class BoolGenerator : IGenerator
    { 
        private Random random = new Random();

        public object Generate()
        {
            return random.Next() % 2 == 0;
        }
    }
}
