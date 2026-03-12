using DocumentFormat.OpenXml.Packaging;

namespace DocumentAnalyzer.Services.Document;
public sealed class DocxTextExtractor : IDocumentTextExtractor
{
    public bool CanHandle(string ext)
        => ext.Equals(".docx", StringComparison.OrdinalIgnoreCase);

    public Task<string> ExtractAsync(Stream stream)
    {
        using var doc = WordprocessingDocument.Open(stream, false);
        return Task.FromResult(
            doc.MainDocumentPart!.Document.Body!.InnerText
        );
    }
}
