﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project_Management_System.Data;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SPMS_Context") ?? throw new InvalidOperationException("Connection string 'SPMS_ContextConnection' not found.");

builder.Services.AddDbContext<SPMS_Context>(options => options.UseSqlServer(connectionString));

builder.Services.AddIdentity<SPMS_User, IdentityRole>(

    options =>
    {
        options.Stores.MaxLengthForKeys = 128;
    })

.AddEntityFrameworkStores<SPMS_Context>()
.AddRoles<IdentityRole>()
.AddDefaultUI()
.AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<Project_Management_SystemContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Project_Management_SystemContext") ?? throw new InvalidOperationException("Connection string 'Project_Management_SystemContext' not found.")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationEndPoint();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<SPMS_Context>();
    context.Database.Migrate();
    var userMgr = services.GetRequiredService<UserManager<SPMS_User>>(); //Implementes HarrysPizzaUser so extra fields are implemented into identity.
    var roleMgr = services.GetRequiredService<RoleManager<IdentityRole>>();
    SPMS_SeedUsers.Initialize(context, userMgr, roleMgr).Wait();
}

app.Run();
