using System.Collections.Generic;

namespace RPS.Api.Models
{
    public class ErrorResponse
    {
        public ErrorResponse()
        {
        }

        public ErrorResponse(List<string> errrors)
        {
            Errors = errrors;
        }

        public List<string> Errors { get; set; }
    }
}
