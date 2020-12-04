﻿using System;

namespace Faker_Lib.FieldGenerators
{
    class ShortGenerator : IGenerator
    {
        private Random random = new Random();

        public object Generate()
        {
            return (short)random.Next();
        }
    }
}
