using BlazorCrudLearning.Services;

var builder = WebApplication.CreateBuilder(args);

// Add Razor Components services so the app can render components and support interactivity.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register our simple in-memory CRUD service.
// Scoped lifetime is usually ideal for app services in Blazor Server.
builder.Services.AddScoped<IProductService, InMemoryProductService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// Map the root Razor component tree.
app.MapRazorComponents<BlazorCrudLearning.Components.App>()
    .AddInteractiveServerRenderMode();

app.Run();
