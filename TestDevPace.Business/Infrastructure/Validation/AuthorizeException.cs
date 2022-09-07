using System.Net;
using System.Runtime.Serialization;

namespace TestDevPace.Business.Infrastructure.Validation
{
    public class AuthorizeException : Exception
    {
        public string? Message { get; protected set; }
        public HttpStatusCode StatusCode { get; protected set; }
        public AuthorizeException()
        {

        }

        public AuthorizeException(string message) : base(message)
        {

        }
        public AuthorizeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
        public AuthorizeException(string message, HttpStatusCode statusCode)
        {
            this.Message = message;
            this.StatusCode = statusCode;
        }
        


    }
}
