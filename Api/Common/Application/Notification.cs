using System;
using System.Collections.Generic;
using System.Linq;
namespace Common.Application{

    public class Notification {
        private List<Error> errors = new List<Error>();

        public void addError(String message) {
            addError(message, null);
        }

        public void addError(String message, Exception e) {
            errors.Add(new Error(message, e));
        }

        public string errorMessage() {
            return string.Join(", ", errors.Select(t => t.message));
        }

        public bool hasErrors() {
            return errors.Any();
        }
    }
}

