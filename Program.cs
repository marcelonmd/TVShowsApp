using TVShows.Components;
using TVShows.Services;
using TVShows.Resources;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<FavoriteService>();
builder.Services.AddScoped<RecentShowsService>();


builder.Services.AddLocalization();

builder.Services.AddHttpClient<ITvMazeService, TvMazeService>(client =>
{
    client.BaseAddress = new Uri("https://api.tvmaze.com/");
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

var supportedCultures = new[] { "en", "pt", "es", "fr" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("en")
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
