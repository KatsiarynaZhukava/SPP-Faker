using System;

namespace Faker_Lib.FieldGenerators
{
    class LongGenerator : ISimpleTypeGenerator
    {
        private Random random = new Random();
        public Type generatedType = typeof(long);
        public Type GeneratedType { get => generatedType; }

        public object Generate()
        {
            return (long)((random.NextDouble() * 2.0 - 1.0) * long.MaxValue);
            /*ulong uRange = 0xFFFFFFFF;
            ulong ulongRand;
            do
            {
                byte[] buf = new byte[8];
                random.NextBytes(buf);
                ulongRand = (ulong)BitConverter.ToInt64(buf, 0);
            } while (ulongRand > ulong.MaxValue - ((ulong.MaxValue % uRange) + 1) % uRange);

            return (long)(ulongRand % uRange);*/
        }
    }
}
