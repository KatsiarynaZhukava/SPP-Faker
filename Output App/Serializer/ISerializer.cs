using System.IO;

namespace Output_App.Serializer
{
    public interface ISerializer
    {
        void Serialize<T>(T toSerialize);
    } 
}