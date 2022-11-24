using System.Diagnostics.CodeAnalysis;

namespace Candidate.Common.Core
{
    [ExcludeFromCodeCoverage]
    public class SuccessResponse<T>
    {
        public T Data { get; set; }
    }
}
