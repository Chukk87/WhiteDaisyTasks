namespace WhiteDaisyLibrary.Services.Interfaces
{
    public interface INameBuilder
    {
        public Task BuildNameAsync(string inputFilePath, string outputFilePath);
    }
}