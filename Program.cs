using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DevOpsNewsApp.Services;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Map("/", async context =>
{
    var newsService = context.RequestServices.GetRequiredService<NewsService>();
    var news = await newsService.GetDevOpsNewsAsync();

    await context.Response.WriteAsync("<html><body>");
    await context.Response.WriteAsync("<h1>DevOps News</h1>");
    await context.Response.WriteAsync("<ul>");

    foreach (var story in news)
    {
        await context.Response.WriteAsync($"<li><a href='{story.Url}'>{story.Title}</a><br>{story.Description}</li>");
    }

    await context.Response.WriteAsync("</ul>");
    await context.Response.WriteAsync("</body></html>");
});

builder.Services.AddSingleton<NewsService>();
builder.Services.AddHttpClient();

app.Run();
