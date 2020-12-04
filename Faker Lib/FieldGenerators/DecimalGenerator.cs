using System;

namespace Faker_Lib.FieldGenerators
{
    class DecimalGenerator : IGenerator
    {
        private Random random = new Random();

        public object Generate()
        {
            DateTime start = new DateTime(1990, 1, 1);
            TimeSpan range = (DateTime.Now - start);
            return start.AddDays(random.Next(range.Days));
        }
    }
}
