using Faker_Lib.FieldGenerators;

namespace Faker_Lib.FieldGenerators
{
    public interface ISimpleTypeGenerator : IGenerator
    {
        object Generate();
    }
}
