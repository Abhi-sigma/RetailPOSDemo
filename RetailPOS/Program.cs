using Microsoft.EntityFrameworkCore;
using RetailPOS.Models;
using RetailPOS.Data;
using RetailPOS.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

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

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Products");
    options.Conventions.AuthorizeFolder("/Sales");
    //options.Conventions.AuthorizeFolder("/Invitations");
    //options.Conventions.AuthorizeFolder("/Occupation");
    //options.Conventions.AuthorizeFolder("/ProgramData").AllowAnonymousToPage("/ProgramData/Index").AllowAnonymousToPage("/ProgramData/Error");
    //options.Conventions.AuthorizeFolder("/VisaCategory");

});
var app = builder.Build();

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
