using System;
using System.Collections.Generic;
using System.Text;

namespace Faker_Lib.FieldGenerators.GenericTypeGenerator
{
    interface IGenericTypeGenerator : IGenerator
    {
        object Generate(Type type);
    }
}
