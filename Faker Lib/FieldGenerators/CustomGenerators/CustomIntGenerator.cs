using System;
using System.Collections.Generic;
using System.Text;

namespace Faker_Lib.FieldGenerators.CustomGenerators
{
    public class CustomIntGenerator : ISimpleTypeGenerator
    {
        private Random random = new Random();
        private Type generatedType = typeof(int);
        public Type GeneratedType { get => generatedType; }
        public int generatedValue = 42;

        public object Generate()
        {
            return generatedValue;
        }
    }
}
