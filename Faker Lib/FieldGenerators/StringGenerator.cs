using System;
using System.Linq;
using System.Text;

namespace Faker_Lib.FieldGenerators
{
    class StringGenerator : ISimpleTypeGenerator
    {
        private Random random = new Random();
        private Type generatedType = typeof(string);
        public Type GeneratedType { get => generatedType; }

        public object Generate()
        {
            int length = random.Next() % 20;

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
