using FakerLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace Faker_Lib.FieldGenerators.GenericTypeGenerator
{
    interface IGenericTypeGenerator
    {
        object Generate(Type type);
        Type generatedType { get; }
        Faker faker { get; }
    }
}
