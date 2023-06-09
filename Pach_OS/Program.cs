using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pach_OS.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<Pach_OSContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<Pach_OSContext>();


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
app.UseAuthentication();;

app.UseAuthorization();

app.UseAuthentication();

app.Use(async (context, next) =>
{
    // Verificar si el usuario no est� autenticado
    if (!context.User.Identity.IsAuthenticated)
    {
        // Verificar si la solicitud no est� en la p�gina de Login o Registro
        if (context.Request.Path != "/Identity/Account/Login" && context.Request.Path != "/Identity/Account/Register")
        {
            // Redirigir a la p�gina de Login
            context.Response.Redirect("/Identity/Account/Login");
            return;
        }
    }

    // Si el usuario est� autenticado o la solicitud est� en la p�gina de Login o Registro,
    // continuar con el siguiente middleware
    await next.Invoke();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
