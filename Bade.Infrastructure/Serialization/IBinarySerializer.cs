namespace Bade.Infrastructure.Serialization
{
    public interface IBinarySerializer
    {

        /// <summary>
        /// Serializes an object graph to a byte sequence.
        /// </summary>
        byte[] Serialize<T>(T data);

        /// <summary>
        /// Deserializes an object graph from the specified binary reader.
        /// </summary>
        T Deserialize<T>(byte[] serialized);
    }
}