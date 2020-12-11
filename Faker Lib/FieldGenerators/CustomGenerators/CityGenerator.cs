using System;
using Faker_Lib.FieldGenerators;

namespace Faker_Lib.FieldGenerators.CustomGenerators
{
    public enum Cities
    {
        TOKYO,
        BERLIN,
        NAIROBI,
        HELSINKI
    }


    public class CityGenerator : ISimpleTypeGenerator
    {
        private Random random = new Random();
        private Type generatedType = typeof(string);
        public Type GeneratedType { get => generatedType; }

        public object Generate()
        {
            return Enum.GetValues(typeof(Cities)).GetValue(random.Next() % Enum.GetValues(typeof(Cities)).Length).ToString();
        }
    }
}
