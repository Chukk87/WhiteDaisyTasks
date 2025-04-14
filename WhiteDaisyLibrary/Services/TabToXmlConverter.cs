using System.Xml;
using WhiteDaisyLibrary.Services.Interfaces;

namespace WhiteDaisyLibrary.Services
{
    public class TabToXmlConverter : ITabToXmlConverter
    {
        public async Task ConvertTabToXmlAsync(string inputPath, string outputPath)
        {
            var dataRows = await ReadTabDelimitedFileAsync(inputPath);
            await CreateXmlFileAsync(dataRows, outputPath);
        }

        /// <summary>
        /// Reads a TAB-delimited file asynchronously and returns a list of rows, 
        /// where each row is a dictionary of column name and value.
        /// </summary>
        private async Task<List<Dictionary<string, string>>> ReadTabDelimitedFileAsync(string filePath)
        {
            var rows = new List<Dictionary<string, string>>();

            using var reader = new StreamReader(filePath);
            string? headerLine = await reader.ReadLineAsync();

            if (string.IsNullOrWhiteSpace(headerLine))
                throw new InvalidDataException("File has no header.");

            var headers = headerLine.Split('\t');

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                if (string.IsNullOrWhiteSpace(line)) continue;

                var values = line.Split('\t');
                var row = new Dictionary<string, string>();

                for (int i = 0; i < headers.Length; i++)
                {
                    string key = headers[i];
                    string value = i < values.Length ? values[i] : string.Empty;
                    row[key] = value;
                }

                rows.Add(row);
            }

            return rows;
        }

        /// <summary>
        /// Converts a list of dictionaries into an XML file asynchronously.
        /// </summary>
        private async Task CreateXmlFileAsync(List<Dictionary<string, string>> rows, string outputFilePath)
        {
            var settings = new XmlWriterSettings
            {
                Async = true,
                Indent = true,
                IndentChars = "  ",
            };

            using var writer = XmlWriter.Create(outputFilePath, settings);

            await writer.WriteStartDocumentAsync();
            await writer.WriteStartElementAsync(null, "DATA", null);

            foreach (var row in rows)
            {
                await writer.WriteStartElementAsync(null, "ROW", null);

                foreach (var column in row)
                {
                    await writer.WriteStartElementAsync(null, column.Key, null);
                    await writer.WriteStringAsync(column.Value);
                    await writer.WriteEndElementAsync(); // column
                }

                await writer.WriteEndElementAsync(); // row
            }

            await writer.WriteEndElementAsync(); // data
            await writer.WriteEndDocumentAsync();
        }
    }
}