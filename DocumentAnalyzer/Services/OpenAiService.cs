using OpenAI;
using OpenAI.Chat;

namespace DocumentAnalyzer.Services;
public class OpenAiService
{
    private readonly OpenAIClient _openAi;

    public OpenAiService(OpenAIClient openAi)
    {
        _openAi = openAi;
    }

    public async Task<string> SummarizeAsync(string documentText)
    {
        try
        {

            ChatClient chatClient = _openAi.GetChatClient("gpt-3.5-turbo");

            ChatCompletion completion = chatClient.CompleteChat(
                [
                    new UserChatMessage("You are a professional document summarizer."),
                    new UserChatMessage($"Summarize this document clearly:\n\n{documentText}")
                ]
            );

            return completion.Content[0].Text;
        }
        catch (Exception e)
        {

            throw;
        }
    }
}
