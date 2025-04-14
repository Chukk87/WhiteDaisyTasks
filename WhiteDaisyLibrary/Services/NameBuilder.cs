using WhiteDaisyLibrary.Classes;
using WhiteDaisyLibrary.Dictionaries;
using WhiteDaisyLibrary.Services.Interfaces;

namespace WhiteDaisyLibrary.Services
{
    public class NameBuilder : INameBuilder
    {
        public async Task BuildNameAsync(string inputFilePath, string outputFilePath)
        {
            var fileLines = await ReadFileAsync(inputFilePath);
            var brokenDownFileLines = ProcessNames(fileLines);
            await WriteOutputAsync(brokenDownFileLines, outputFilePath);
        }

        // Read lines of data from the input file asynchronously
        private async Task<List<string>> ReadFileAsync(string filePath)
        {
            var lines = await File.ReadAllLinesAsync(filePath);
            return lines.Select(line => line.Trim()).ToList();
        }

        // Process each name and break it down into parts
        private List<string> ProcessNames(List<string> names)
        {
            var processedNames = new List<string>();

            foreach (var name in names)
            {
                var parts = SplitName(name);
                var formattedName = FormatNameOutput(parts);
                processedNames.Add(formattedName);
            }

            return processedNames;
        }

        // Populate the data into the NameData class
        private NameData SplitName(string name)
        {
            var nameParts = name.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var nameData = new NameData();

            if (nameParts.Length == 0)
                return nameData;

            if (NameAffix.NameAffixDictionary["Title"].Contains(nameParts[0].ToUpper()))
            {
                nameData.Title = nameParts[0];
                nameParts = nameParts[1..];
            }

            if (nameParts.Length > 0 &&
                NameAffix.NameAffixDictionary["Suffix"].Contains(nameParts[^1].ToUpper()))
            {
                nameData.Suffix = nameParts[^1];
                nameParts = nameParts[..^1];
            }

            if (nameParts.Length > 0)
                nameData.FirstName = nameParts[0];
            if (nameParts.Length > 1)
                nameData.LastName = nameParts[^1];
            if (nameParts.Length > 2)
                nameData.MiddleNames = string.Join(" ", nameParts[1..^1]);

            return nameData;
        }

        // Format the data from the NameData class
        private static string FormatNameOutput(NameData nameData)
        {
            return string.Join(',', nameData.Title, nameData.FirstName, nameData.MiddleNames, nameData.LastName, nameData.Suffix);
        }

        // Write data to the output file asynchronously
        private static async Task WriteOutputAsync(List<string> brokenDownNames, string filePath)
        {
            await File.WriteAllLinesAsync(filePath, brokenDownNames);
        }
    }
}