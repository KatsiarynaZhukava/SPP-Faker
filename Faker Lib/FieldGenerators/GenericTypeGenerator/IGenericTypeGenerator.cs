using FakerLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace Faker_Lib.FieldGenerators.GenericTypeGenerator
{
    public interface IGenericTypeGenerator
    {
        Type GeneratedType { get; }

        object Generate(Type type);
    }
}
