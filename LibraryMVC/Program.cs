using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LibraryMVC.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
  


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
app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//Adding roles into code - test data 

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();


    var roles = new[] { "Admin", "Manager", "Member" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
    
}

using (var scope  = app.Services.CreateScope())
{

    var userManager = 
        scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    
    string emailAdmin = "admin@admin.com";
    string passwordAdmin = "Test123!";
    
    string emailManager = "manager@manager.com";
    string passwordManager = "Test123!";
    
    string emailMember = "member@member.com";
    string passwordMember = "Test123!";

    if (await userManager.FindByEmailAsync(emailAdmin) == null)
    {
        var userAdmin = new ApplicationUser();

        userAdmin.UserName = emailAdmin;
        userAdmin.Email = emailAdmin;
        userAdmin.FirstName = "Admin";
        userAdmin.LastName = "admin";

        await userManager.CreateAsync(userAdmin, passwordAdmin);


        await userManager.AddToRoleAsync(userAdmin, "Admin");
    }

    if (await userManager.FindByEmailAsync(emailManager) == null){

        var userManagerTest = new ApplicationUser();

        userManagerTest.UserName = emailManager;
        userManagerTest.Email = emailManager;
        userManagerTest.FirstName = "Manager";
        userManagerTest.LastName = "Manager";
        await userManager.CreateAsync(userManagerTest, passwordManager);

        await userManager.AddToRoleAsync(userManagerTest, "Manager");
    }
    if (await userManager.FindByEmailAsync(emailMember) == null){

        var userMemberTest = new ApplicationUser();
        
        userMemberTest.UserName = emailMember;
        userMemberTest.Email = emailMember;
        userMemberTest.FirstName = "Member";
        userMemberTest.LastName = "Member";
        
        await userManager.CreateAsync(userMemberTest, passwordMember);
        
        await userManager.AddToRoleAsync(userMemberTest, "Member");
    }

}

app.Run();