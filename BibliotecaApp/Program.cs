using Microsoft.EntityFrameworkCore;
using BibliotecaApp.Data;
using BibliotecaApp.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BibliotecaApp.Models;
using Microsoft.AspNetCore.Authorization;
using BibliotecaApp.Handlers;
using BibliotecaApp.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});
builder.Services.AddCors(options => options.AddPolicy(
    "CorsPolicies", b => b
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
        .SetIsOriginAllowed(_ => true)
));
builder.Services.AddDbContext<BibliotecaAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BibliotecaAppContext") ?? throw new InvalidOperationException("Connection string 'BibliotecaAppContext' not found.")));

builder.Services.AddIdentity<Usuario, IdentityRole>(options =>
    {
        options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ ";
    })
    .AddEntityFrameworkStores<BibliotecaAppContext>() // inclusão do pacote Microsoft.AspNetCore.Identity.EntityFrameworkCore
    .AddDefaultTokenProviders();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login";
    options.AccessDeniedPath = "/Login";
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ClientOnly", policy => policy.Requirements.Add(new ClientOnlyRequirement()));
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Administrador"));
});

builder.Services.AddSingleton<IAuthorizationHandler, ClientOnlyHandler>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        // Obter o RoleManager eo Usermanager
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<Usuario>>();


        // Chamada dos métodos de Seed
        await SeedConfiguration.SeedRoles(roleManager);
        await SeedConfiguration.SeedUsers(userManager); 

    } catch (Exception e) { 
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(e, "Ocorreu um erro durante o seeding inicial.");
    }
}

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapRazorPages();

app.UseCors("CorsPolicies");

app.UseMiddleware<RequestLoggingMiddleware>();

app.Run();