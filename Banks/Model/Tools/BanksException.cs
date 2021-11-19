using System;
using System.Runtime.Serialization;

namespace Banks.Model.Tools
{
    public class BanksException : Exception
    {
        public BanksException()
        {
        }

        public BanksException(string message)
            : base(message)
        {
        }

        public BanksException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected BanksException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}