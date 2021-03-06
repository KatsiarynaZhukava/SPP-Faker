﻿using System;

namespace Faker_Lib.FieldGenerators
{
    class ULongGenerator : ISimpleTypeGenerator
    {
        private Random random = new Random();
        private Type generatedType = typeof(ulong);
        public Type GeneratedType { get => generatedType; }

        public object Generate()
        {
            var bytes = new byte[sizeof(ulong)];
            random.NextBytes(bytes);
            return BitConverter.ToUInt64(bytes, 0);

            /*ulong uRange = 0xFFFFFFFF;
            ulong ulongRand;
            do
            {
                byte[] buf = new byte[8];
                random.NextBytes(buf);
                ulongRand = (ulong)BitConverter.ToInt64(buf, 0);
            } while (ulongRand > ulong.MaxValue - ((ulong.MaxValue % uRange) + 1) % uRange);

            return ulongRand % uRange;*/
        }
    }
}
