using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using NucliaDb;

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
builder.Services.AddSingleton(config);
builder.Services.AddScoped<NucliaDbClient>();

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
