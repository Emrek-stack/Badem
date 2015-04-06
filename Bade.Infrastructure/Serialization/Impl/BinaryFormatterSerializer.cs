using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Bade.Infrastructure.Serialization.Impl
{
    public class BinaryFormatterSerializer : IBinarySerializer
    {
        private readonly BinaryFormatter _formatter;

        public BinaryFormatterSerializer()
        {
            _formatter = new BinaryFormatter();
        }

        public byte[] Serialize<T>(T graph)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                _formatter.Serialize(ms, graph);
                return ms.ToArray();
            }
        }

        public T Deserialize<T>(byte[] serialized)
        {
            try
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    memStream.Write(serialized, 0, serialized.Length);
                    memStream.Seek(0, SeekOrigin.Begin);
                    T obj = (T)_formatter.Deserialize(memStream);
                    return obj;
                }
            }
            catch (SerializationException e)
            {
                // Wrap in a standard .NET exception.
                throw new SerializationException(e.Message, e);
            }
        }
    }
}