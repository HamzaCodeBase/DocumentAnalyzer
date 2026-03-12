using System.Text;
using UglyToad.PdfPig;

namespace DocumentAnalyzer.Services.Document;

public class PdfTextExtractor : IDocumentTextExtractor
{
    public bool CanHandle(string ext)
       => ext.Equals(".pdf", StringComparison.OrdinalIgnoreCase);

    public Task<string> ExtractAsync(Stream stream)
    {
        var sb = new StringBuilder();

        using var doc = PdfDocument.Open(stream);
        foreach (var page in doc.GetPages())
            sb.AppendLine(page.Text);

        return Task.FromResult(sb.ToString());
    }
}
