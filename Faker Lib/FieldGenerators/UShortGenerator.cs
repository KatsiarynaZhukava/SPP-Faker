﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Faker_Lib.FieldGenerators
{
    class UShortGenerator : IGenerator
    {
        private Random random = new Random();
        public Type generatedType { get; private set; }

        public object Generate()
        {
            return (ushort)random.Next();
        }
    }
}
