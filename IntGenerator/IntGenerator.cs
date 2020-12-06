using System;

namespace IntGenerator
{
    public class IntGenerator
    {
        private Random random = new Random();
        public Type generatedType { get; private set; }

        public object Generate()
        {
            return random.Next();
        }
    }
}
