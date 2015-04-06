using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace Bade.Infrastructure.Serialization.Impl
{
public class XmlTextSerializer : ITextSerializer
    {
        private readonly XmlWriterSettings _settings = new XmlWriterSettings();

        public XmlTextSerializer()
            : this(new XmlWriterSettings()
            {
#if DEBUG
                Indent = true,
                IndentChars = " ",
#endif
            })
        { }

        public XmlTextSerializer(XmlWriterSettings settings)
        {
            _settings = settings;
        }

        public void Serialize(TextWriter writer, object graph)
        {
            var xwriter = XmlWriter.Create(writer, _settings);
            new XmlSerializer(graph.GetType()).Serialize(xwriter, graph);
            writer.Flush();
        }

        public object Deserialize(TextReader reader, Type t)
        {
            return new XmlSerializer(t).Deserialize(reader);
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
            try
            {
                using (var reader = new StringReader(serialized))
                {
                    return (T)Deserialize(reader, typeof(T));
                }
            }
            catch (XmlException e)
            {
                throw new SerializationException(e.Message, e);
            }
        }
    }
}