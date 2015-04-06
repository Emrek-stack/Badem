namespace Bade.Infrastructure.Serialization
{
    public interface ITextSerializer
    {
        /// <summary>
        /// Serializes an object graph to a text reader.
        /// </summary>
        string Serialize<T>(T data);

        /// <summary>
        /// Deserializes an object graph from the specified text reader.
        /// </summary>
        T Deserialize<T>(string serialized);
    }
}