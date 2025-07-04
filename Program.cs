using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniAccountSystem.Data;
using MiniAccountSystem.Services.AccountChartList;
using MiniAccountSystem.Services.ModuleAccess;
using MiniAccountSystem.Services.Vouchers;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 3;
});

builder.Services.AddScoped<IAccountChartService, AccountChartService>();
builder.Services.AddScoped<IModuleAccessService, ModuleAccessService>();
builder.Services.AddScoped<IVoucherService, VoucherService>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    string[] roles = { "Admin", "Accountant", "Viewer" };

    foreach (string role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }

    var adminEmail = "admin@qtec.com";
    var adminPassword = "Hey@123";

    var user = await userManager.FindByEmailAsync(adminEmail);
    if (user == null)
    {
        var adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail };
        await userManager.CreateAsync(adminUser, adminPassword);
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }
    var accountantEmail = "accountant@demo.com";
    var accountantPassword = "Acc@123";

    var accountantUser = await userManager.FindByEmailAsync(accountantEmail);
    if (accountantUser == null)
    {
        accountantUser = new IdentityUser { UserName = accountantEmail, Email = accountantEmail };
        await userManager.CreateAsync(accountantUser, accountantPassword);
        await userManager.AddToRoleAsync(accountantUser, "Accountant");
    }

    var viewerEmail = "viewer@demo.com";
    var viewerPassword = "View@123";

    var viewerUser = await userManager.FindByEmailAsync(viewerEmail);
    if (viewerUser == null)
    {
        viewerUser = new IdentityUser { UserName = viewerEmail, Email = viewerEmail };
        await userManager.CreateAsync(viewerUser, viewerPassword);
        await userManager.AddToRoleAsync(viewerUser, "Viewer");
    }

}
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
