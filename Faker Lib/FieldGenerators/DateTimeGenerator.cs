using System;
using System.Collections.Generic;
using System.Text;

namespace Faker_Lib.FieldGenerators
{
    class DateTimeGenerator : ISimpleTypeGenerator
    {
        private Random random = new Random();
        public Type generatedType = typeof(DateTime);
        public Type GeneratedType { get => generatedType; }
        public object Generate()
        {
            DateTime start = new DateTime(1800, 1, 1);
            TimeSpan range = (DateTime.Now - start);
            return start.AddDays(random.Next(range.Days));
        }       
    }
}
