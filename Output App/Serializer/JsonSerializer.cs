using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Output_App.Serializer
{
    public class JsonSerializer : ISerializer
    {
        void ISerializer.Serialize<T>(T toSerialize)
        {
            using (var jsonWriter = JsonReaderWriterFactory.CreateJsonWriter(Console.OpenStandardOutput(), Encoding.UTF8, ownsStream: true, indent: true))
            {
                new DataContractJsonSerializer(typeof(T)
                     , new DataContractJsonSerializerSettings
                     {
                         DateTimeFormat = new DateTimeFormat("dd.MM.yyyy'  'HH:mm:ss")
                     }
              ).WriteObject(jsonWriter, toSerialize);
            }
        }
    }
}