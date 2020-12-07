using System;

namespace Faker_Lib.FieldGenerators
{
    class UIntGenerator : ISimpleTypeGenerator
    {
        private Random random = new Random();
        public Type generatedType = typeof(uint);
        public Type GeneratedType { get => generatedType; }

        public object Generate()
        {
            return (uint)(random.Next(1 << 30)) << 2 | (uint)(random.Next(1 << 2));
        }
    }
}
