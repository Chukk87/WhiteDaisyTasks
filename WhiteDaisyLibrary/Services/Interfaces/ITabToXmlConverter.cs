namespace WhiteDaisyLibrary.Services.Interfaces
{
    public interface ITabToXmlConverter
    {
        public Task ConvertTabToXmlAsync(string inputPath, string outputPath);
    }
}