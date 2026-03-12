namespace DocumentAnalyzer.Services.Document;

public interface IDocumentTextExtractor
{
    bool CanHandle(string extension);
    Task<string> ExtractAsync(Stream stream);
}
