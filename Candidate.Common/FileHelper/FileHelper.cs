using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Candidate.Common.FileHelper
{
    [ExcludeFromCodeCoverage]
    public static class FileHelper
    {
        public static void CreateDirectory(string directory)
        {
            if (!Directory.Exists(directory) && !string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        public static void CreateFile(string filePath)
        {
            if (!File.Exists(filePath) && !string.IsNullOrEmpty(filePath))
            {
                File.Create(filePath).Close();
                AddCsvFileHeader(filePath);

            }
        }

        private static void AddCsvFileHeader(string filePath)
        {
            using var streamWriter = new StreamWriter(filePath);
            streamWriter.WriteLine("Email, FirstName, LastName, PhoneNumber, LinkedInUrl, GithubUrl, From, To, Comment");
        }
    }
}
