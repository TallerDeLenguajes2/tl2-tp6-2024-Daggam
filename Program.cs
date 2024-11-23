using PresupuestoRepositoryNamespace;
using ProductoRepositoryNamespace;
using UsuarioRepositoryNamespace;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//Agregamos los servicios de sesiones aquí.
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(300);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
//Agregamos los repositorios aquí.
builder.Services.AddScoped<IPresupuestoRepository,SQLitePresupuestoRepository>();
builder.Services.AddScoped<IProductoRepository,SQLiteProductoRepository>();
builder.Services.AddScoped<IUsuarioRepository,SQLiteUsuarioRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
//Agregamos el middleware de sesiones.
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
