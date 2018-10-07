using System;
namespace Common.Application{
    public class Error {
        
        public string message { get; set; }
        public Exception cause { get; set; }

        public Error(string message, Exception cause) {
            this.message = message;
            this.cause = cause;
        }

    }

}
