using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Models
{
    public class ErrorResponse
    {
        public ErrorResponse()
        {
            CorrelationId = Guid.NewGuid();
            Errors = new List<KeyValuePair<string, string>>();
        }
        public ErrorResponse(string location, string message)
        {
            CorrelationId = Guid.NewGuid();
            Errors = new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>(location, message) };
        }

        public Guid CorrelationId { get; set; }
        public List<KeyValuePair<string, string>> Errors { get; }
        public void AddError(string location, string message)
        {
            Errors.Add(new KeyValuePair<string, string>(location, message));
        }
    }
}
