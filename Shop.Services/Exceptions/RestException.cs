using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Services.Exceptions
{
    public class RestException:Exception
    {
        public RestException(HttpStatusCode statusCode,string errorItemKey,string errorItemMessage, string message=null)
        {
            StatusCode = statusCode;
            Message = message;
            Errors = new List<RestExceptionError> { new RestExceptionError (errorItemKey, errorItemMessage) };
        }
        public RestException(HttpStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
           
        }
        public RestException(HttpStatusCode statusCode, List<RestExceptionError> errors)
        {
            StatusCode = statusCode;
            Errors = errors;
        }
        public HttpStatusCode StatusCode { get; set; }
        public string Message {get;set;}

        public List<RestExceptionError> Errors { get; set;}
    }
    public class RestExceptionError
    {
        public RestExceptionError(string key,string message)
        {
            Key= key;
            Message= message;
        }
        public string Key { get; set; }
        public string Message { get; set; }
    }
}
