using DocumentAnalyzer.Components;
using DocumentAnalyzer.Services;
using DocumentAnalyzer.Services.Document;
using OpenAI;

namespace DocumentAnalyzer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddSingleton(new OpenAIClient(builder.Configuration["OpenAI:ApiKey"]));
            builder.Services.AddScoped<IDocumentTextExtractor, PdfTextExtractor>();
            builder.Services.AddScoped<IDocumentTextExtractor, DocxTextExtractor>();
            builder.Services.AddScoped<OpenAiService>();
            builder.Services.AddScoped<DocumentProcessor>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
