using Faker_Lib;
using System;
using System.Collections.Generic;
using System.Text;

namespace Faker_Lib.FieldGenerators.GenericTypeGenerator
{
    public interface IGenericTypeGenerator : IGenerator
    {
        Type GeneratedType { get; }
         

        object Generate(Type type, Faker faker);
    }
}
