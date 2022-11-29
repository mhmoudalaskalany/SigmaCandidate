using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Candidate.Common.FileHelper
{
    [ExcludeFromCodeCoverage]
    public static class CsvRowParser
    {
        public static Domain.Entities.Candidate ParseRow(string line)
        {
            try
            {
                var candidate = new Domain.Entities.Candidate();
                var columns = line.Split(',').ToList();
                if (columns.Count() != 9) throw  new InvalidOperationException("Invalid Row");
                candidate.Comment = columns[0];
                candidate.Email = columns[1];
                candidate.FirstName = columns[2];
                candidate.From = TimeSpan.Parse(columns[3]);
                candidate.GithubUrl = columns[4];
                candidate.LastName = columns[5];
                candidate.LinkedInUrl = columns[6];
                candidate.PhoneNumber = columns[7];
                candidate.To = TimeSpan.Parse(columns[8]);
                return candidate;

            }
            catch (Exception)
            {
                throw new InvalidOperationException("Unable To pares Row");
            }
        }
    }
}
