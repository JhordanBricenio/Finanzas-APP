using Finanzas_APP.DB;
using Finanzas_APP.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ITipoCuentaRepository, TipoCuentaRepository>();
builder.Services.AddTransient<ICuentaRepository, CuentaRepository>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepo>();
builder.Services.AddTransient<ICuentaTransacRepo, CuentaTransacRepo>();



builder.Services.AddDbContext<BbEntities>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("con")));

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Cuenta}/{action=Index}");

app.Run();
