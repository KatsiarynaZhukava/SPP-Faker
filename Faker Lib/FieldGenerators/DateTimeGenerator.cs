using System;
using System.Collections.Generic;
using System.Text;

namespace Faker_Lib.FieldGenerators
{
    class DateTimeGenerator : IGenerator
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
