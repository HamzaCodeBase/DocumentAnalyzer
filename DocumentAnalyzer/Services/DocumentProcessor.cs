using DocumentAnalyzer.Services.Document;
using System.Text;

namespace DocumentAnalyzer.Services;
public sealed class DocumentProcessor
{
    private readonly IEnumerable<IDocumentTextExtractor> _extractors;
    private readonly OpenAiService _openAi;

    public DocumentProcessor(
        IEnumerable<IDocumentTextExtractor> extractors,
        OpenAiService openAi)
    {
        _extractors = extractors;
        _openAi = openAi;
    }
    public async Task<string> ProcessAsync(Stream fileStream, string fileName)
    {
        var ext = Path.GetExtension(fileName).ToLowerInvariant();

        var extractor = _extractors.FirstOrDefault(e => e.CanHandle(ext))
            ?? throw new NotSupportedException("Unsupported file type");

        var text = await extractor.ExtractAsync(fileStream);

        var result = await _openAi.SummarizeAsync(text);

        return result;
    }
}
