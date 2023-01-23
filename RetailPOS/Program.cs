using Microsoft.EntityFrameworkCore;
using RetailPOS.Models;
using RetailPOS.Data;
using RetailPOS.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("RetailPOSAuthContextConnection") ?? throw new InvalidOperationException("Connection string 'RetailPOSAuthContextConnection' not found.");

// Add services to the container.
builder.Services.AddRazorPages();


builder.Services.AddDbContext<RetailPOSContext>(
        options => options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Database=RetailPOS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));

builder.Services.AddDbContext<RetailDbContext>(
        options => options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Database=RetailPOS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));

builder.Services.AddIdentity<RetailPOSUser,IdentityRole>()
            .AddEntityFrameworkStores<RetailPOSContext>()
            .AddDefaultTokenProviders();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdministratorRole",
         policy => policy.RequireRole("Administrator"));
});

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Products", "RequireAdministratorRole");
    options.Conventions.AuthorizeFolder("/Sales/Offline/Create");
    options.Conventions.AuthorizeFolder("/Sales/Offline/Edit", "RequireAdministratorRole");
    options.Conventions.AuthorizeFolder("/Sales/Offline/Delete","RequireAdministratorRole");
    

   

});
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<RetailDbContext>();
    await DbInitializer.Initialize(scope.ServiceProvider);

}




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
app.UseAuthentication();;
app.UseAuthorization();

app.MapRazorPages();

app.Run();
