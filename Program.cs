using ChillSpot.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Obtener la cadena de conexi�n desde la configuraci�n
var connectionString = builder.Configuration["ConexionSql"];

// Registrar el DbContext usando la cadena de conexi�n
builder.Services.AddDbContext<chillSpotDbContext>(options =>
    options.UseSqlServer(connectionString));

// Agregar servicios de Blazor y MVC
builder.Services.AddControllersWithViews();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configurar el pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Si usas Blazor Server, mapea el componente ra�z (descomenta si tienes App.razor)
// app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
