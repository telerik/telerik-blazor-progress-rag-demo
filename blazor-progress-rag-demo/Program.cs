using blazor_progress_rag_demo.Services;
using Progress.Nuclia;
using Progress.Nuclia.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddTelerikBlazor();

// Configure NucliaDB client using Options pattern
var config = builder.Configuration.GetSection("NucliaDb").Get<NucliaDbConfig>()
    ?? throw new InvalidOperationException("NucliaDb configuration is missing or invalid");

var chartsConfig = builder.Configuration.GetSection("NucliaDbCharts").Get<NucliaDbConfig>()
    ?? throw new InvalidOperationException("NucliaDbCharts configuration is missing or invalid");

var verseConfig = builder.Configuration.GetSection("NucliaDbVerse").Get<NucliaDbConfig>()
    ?? throw new InvalidOperationException("NucliaDbVerse configuration is missing or invalid");

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
