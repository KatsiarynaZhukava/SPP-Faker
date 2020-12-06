using System;
using System.Text;

namespace Faker_Lib.FieldGenerators
{
    class StringGenerator : IGenerator
    {
        private Random random = new Random();
        public Type generatedType { get; private set; }

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
