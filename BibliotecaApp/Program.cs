﻿using Microsoft.EntityFrameworkCore;
using BibliotecaApp.Data;
using BibliotecaApp.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BibliotecaApp.Models;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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