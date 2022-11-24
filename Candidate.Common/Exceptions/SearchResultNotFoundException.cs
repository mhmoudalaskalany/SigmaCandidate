using System.Diagnostics.CodeAnalysis;

namespace Candidate.Common.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class SearchResultNotFoundException :BaseException
    {
        public SearchResultNotFoundException():base("Result not found")
        {
                
        }
    }
}
