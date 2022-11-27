using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

        public static async Task WriteCsv<T>(T entity, string path)
        {
            var props = GetProps<T>();
            var stringBuilder = new StringBuilder();
            var line = string.Join(", ", props.Select(p => p.GetValue(entity, null)));
            stringBuilder.AppendLine(line);
            await File.AppendAllTextAsync(path, stringBuilder.ToString());

        }


        private static void AddCsvFileHeader(string filePath)
        {
            var props = GetProps<Domain.Entities.Candidate>();
            using var streamWriter = new StreamWriter(filePath);
            var line = string.Join(", ", props.Select(p => p.Name));
            streamWriter.WriteLine(line);
        }

        private static IOrderedEnumerable<PropertyInfo> GetProps<T>()
        {
            var itemType = typeof(T);
            var props = itemType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .OrderBy(p => p.Name);
            return props;
        }


        
    }
}
