using blazor_progress_rag_demo.Services;
using NucliaDb;
using NucliaDb.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddTelerikBlazor();

// Configure NucliaDB client
var nucleiaConfig = builder.Configuration.GetSection("NucliaDb");
var config = new NucliaDbConfig(
    nucleiaConfig["ZoneId"] ?? throw new InvalidOperationException("NucliaDb ZoneId not configured"),
    nucleiaConfig["KnowledgeBoxId"] ?? throw new InvalidOperationException("NucliaDb KnowledgeBoxId not configured"),
    nucleiaConfig["ApiKey"] ?? throw new InvalidOperationException("NucliaDb ApiKey not configured")
);

var nucleiaChartsConfig = builder.Configuration.GetSection("NucliaDbCharts");
var chartsConfig = new NucliaDbConfig(
    nucleiaChartsConfig["ZoneId"] ?? throw new InvalidOperationException("NucliaDbCharts ZoneId not configured"),
    nucleiaChartsConfig["KnowledgeBoxId"] ?? throw new InvalidOperationException("NucliaDbCharts KnowledgeBoxId not configured"),
    nucleiaChartsConfig["ApiKey"] ?? throw new InvalidOperationException("NucliaDbCharts ApiKey not configured")
);

var nucleiaVerseConfig = builder.Configuration.GetSection("NucliaDbVerse");
var verseConfig = new NucliaDbConfig(
    nucleiaVerseConfig["ZoneId"] ?? throw new InvalidOperationException("NucliaDbVerse ZoneId not configured"),
    nucleiaVerseConfig["KnowledgeBoxId"] ?? throw new InvalidOperationException("NucliaDbVerse KnowledgeBoxId not configured"),
    nucleiaVerseConfig["ApiKey"] ?? throw new InvalidOperationException("NucliaDbVerse ApiKey not configured")
);

builder.Services.AddKeyedNucliaDb("default", config);
builder.Services.AddKeyedNucliaDb("charts", chartsConfig);
builder.Services.AddKeyedNucliaDb("verse", verseConfig);
builder.Services.AddScoped<NucliaSearchService>();

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

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
