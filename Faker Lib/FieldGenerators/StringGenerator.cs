using System;
using System.Text;

namespace Faker_Lib.FieldGenerators
{
    class StringGenerator : ISimpleTypeGenerator
    {
        private Random random = new Random();
        public Type generatedType = typeof(string);
        public Type GeneratedType { get => generatedType; }

        public object Generate()
        {
            int length = random.Next();
            StringBuilder generatedString = new StringBuilder();
            char letter;

            for (int i = 0; i < length; i++)
            {
                int shift = Convert.ToInt32(Math.Floor(25 * random.NextDouble()));
                letter = Convert.ToChar(shift + 65);
                generatedString.Append(letter);
            }
            return generatedString.ToString();
        }
    }
}
