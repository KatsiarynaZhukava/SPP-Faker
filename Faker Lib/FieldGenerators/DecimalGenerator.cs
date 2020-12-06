using System;

namespace Faker_Lib.FieldGenerators
{
    internal class DecimalGenerator : IGenerator
    {
        private Random random = new Random();
        public Type generatedType { get; private set; }

        public int NextInt32()
        {
            int firstBits = random.Next(0, 1 << 4) << 28;
            int lastBits = random.Next(0, 1 << 28);
            return firstBits | lastBits;
        }

        public object Generate()
        {
            byte scale = (byte)random.Next(29);
            bool sign = random.Next(2) == 1;
            return new decimal(NextInt32(),
                               NextInt32(),
                               NextInt32(),
                               sign,
                               scale);
        }
    }
}
