using System;
using Bade.Constants;

namespace Bade.Infrastructure
{
    [Serializable]
    public class ResultSet
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public ResultSet()
        {
            Success = false;
            Message = Messages.Error.Default;
        }
    }

    /// <summary>
    /// Varsayılan değerleri 
    /// Success = false
    /// Message = "İşleminiz gerçekleştirilemedi"
    /// </summary>
    [Serializable]
    public class ResultSet<T> : ResultSet
    {
        public T Object { get; set; }
    }
}