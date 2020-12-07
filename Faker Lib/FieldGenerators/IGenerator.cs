using System;

namespace Faker_Lib.FieldGenerators
{ 
    public interface IGenerator
    {
        Type GeneratedType { get; }
    }
}
