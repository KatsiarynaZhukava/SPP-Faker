﻿using System;

namespace Faker_Lib.FieldGenerators
{
    class UIntGenerator : IGenerator
    {
        private Random random = new Random();

        public object Generate()
        {
            return (uint)(random.Next(1 << 30)) << 2 | (uint)(random.Next(1 << 2));
        }
    }
}
