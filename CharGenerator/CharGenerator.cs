using Faker_Lib.FieldGenerators;
using System;

namespace CharGenerator
{
    public class CharGenerator : ISimpleTypeGenerator
    {
        private Random random = new Random();
        public Type generatedType = typeof(char);
        public Type GeneratedType { get => generatedType; }

        public object Generate()
        {
            char[] base64Representation = new char[4];
            Convert.ToBase64CharArray(inArray: new byte[] { (byte)random.Next() }, offsetIn: 0, length: 1, outArray: base64Representation, offsetOut: 0);
            return base64Representation[random.Next(0, 2)];
        }
    }
}
