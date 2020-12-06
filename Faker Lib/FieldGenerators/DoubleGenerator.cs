﻿using System;


namespace Faker_Lib.FieldGenerators
{
    class DoubleGenerator : IGenerator
    {
        private Random random = new Random();
        public Type generatedType { get; private set; }

        public object Generate()
        {
            return random.NextDouble();
        }
    }
}
