using System.IO;
using System.Runtime.Serialization;
using Newtonsoft.Json;


namespace Bade.Infrastructure.Serialization.Impl
{
    public class JsonTextSerializer : ITextSerializer
    {
        private readonly JsonSerializer _serializer;

        public JsonTextSerializer()
            : this(JsonSerializer.Create(new JsonSerializerSettings
            {
                // Allows deserializing to the actual runtime type
                TypeNameHandling = TypeNameHandling.All,
                // In a version resilient way
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            }))
        {
        }

        public JsonTextSerializer(JsonSerializer serializer)
        {
            _serializer = serializer;
        }

        public void Serialize(TextWriter writer, object graph)
        {
            var jsonWriter = new JsonTextWriter(writer) { Formatting = Formatting.Indented };
#if DEBUG
#endif

            _serializer.Serialize(jsonWriter, graph);

            // We don't close the stream as it's owned by the message.
            writer.Flush();
        }

        public object Deserialize(TextReader reader)
        {
            var jsonReader = new JsonTextReader(reader);

            try
            {
                return _serializer.Deserialize(jsonReader);
            }
            catch (JsonSerializationException e)
            {
                // Wrap in a standard .NET exception.
                throw new SerializationException(e.Message, e);
            }
        }

        public string Serialize<T>(T data)
        {
            using (var writer = new StringWriter())
            {
                Serialize(writer, data);
                return writer.ToString();
            }
        }

        public T Deserialize<T>(string serialized)
        {
            using (var reader = new StringReader(serialized))
            {
                return (T)_serializer.Deserialize(reader, typeof(T));
            }
        }
    }
}