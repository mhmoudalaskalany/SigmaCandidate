using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Candidate.Common.Core
{
    [ExcludeFromCodeCoverage]
    public class ErrorResponse
    {
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
    }
}
