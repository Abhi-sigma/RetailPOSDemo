
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RetailPOS.Areas.Identity.Data;

namespace RetailPOS.Data;

public class RetailPOSContext : IdentityDbContext<RetailPOSUser>
{
    public RetailPOSContext(DbContextOptions<RetailPOSContext> options)
        : base(options)
    {

    }
   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        //Seeding a  'Administrator' role to AspNetRoles table
        //modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "2c5e174e-3b0e-446f-86af-483d56fd7210", Name = "Administrator", NormalizedName = "ADMINISTRATOR".ToUpper() });


        //a hasher to hash the password before seeding the user to the db
        //var hasher = new PasswordHasher<IdentityUser>();


        //Seeding the User to AspNetUsers table
        //modelBuilder.Entity<IdentityUser>().HasData(
        //    new IdentityUser
        //    {
        //        Id = "8e445865-a24d-4543-a6c6-9443d048cdb9", // primary key
        //        UserName = "demo",
        //        NormalizedUserName = "de",
        //        PasswordHash = hasher.HashPassword(null, "demo123#")
        //    }
        //);


        ////Seeding the relation between our user and role to AspNetUserRoles table
        //modelBuilder.Entity<IdentityUserRole<string>>().HasData(
        //    new IdentityUserRole<string>
        //    {
        //        RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
        //        UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
        //    }
        //);


    }
}
