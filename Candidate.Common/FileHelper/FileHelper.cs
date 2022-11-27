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

        public static async Task<bool> IsExist(string path, string email)
        {
            var lines = await File.ReadAllLinesAsync(path);
            var exist = lines.FirstOrDefault(x => x.Split(',')[1].Trim() == email.Trim());
            return !string.IsNullOrEmpty(exist);
        }

        public static async Task<string> GetSingleLine(string path, string email)
        {
            var lines = await File.ReadAllLinesAsync(path);
            return lines.FirstOrDefault(x => x.Split(',')[1].Trim() == email.Trim());
        }

        public static async Task DeleteLine(string path, string email)
        {
            var lines = await File.ReadAllLinesAsync(path);
            var filteredLines = lines.Where(x => x.Split(',')[1].Trim() != email.Trim()).ToArray();
            await File.WriteAllLinesAsync(path, filteredLines);
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
