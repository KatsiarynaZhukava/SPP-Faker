using System;

namespace CharGenerator
{
    public class CharGenerator
    {
        private Random random = new Random();
        public Type generatedType { get; private set; }

        public object Generate()
        {
            return (char)random.Next(0xFF);
        }
    }
}
