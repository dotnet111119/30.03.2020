using System;
using System.Runtime.Serialization;

namespace FlightCenter
{
    [Serializable]
    public class AirlineAlreadyExistException : Exception
    {
        public AirlineAlreadyExistException()
        {
        }

        public AirlineAlreadyExistException(string message) : base(message)
        {
        }

        public AirlineAlreadyExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AirlineAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}