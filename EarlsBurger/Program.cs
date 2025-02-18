using Microsoft.EntityFrameworkCore;
using EarlsBurger.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<EarlsBurgerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EarlsBurgerContext") ?? throw new InvalidOperationException("Connection string 'EarlsBurgerContext' not found.")));

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<EarlsBurgerContext>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization(options=>
{
    options.AddPolicy("RequireAdmins", policy => policy.RequireRole("Admin"));

});
builder.Services.AddRazorPages()
    .AddRazorPagesOptions(options =>
    {
        options.Conventions.AuthorizeFolder("/Admin", "RequireAdmins");
    });

builder.Services.AddIdentity<IdentityUser, IdentityRole>(
        options =>
        {
            options.Stores.MaxLengthForKeys = 128;
        })
    .AddEntityFrameworkStores<EarlsBurgerContext>()
    .AddRoles<IdentityRole>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

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
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<EarlsBurgerContext>();
    context.Database.Migrate();
    var userMgr = services.GetRequiredService<UserManager<IdentityUser>>();
    var roleMgr = services.GetRequiredService<RoleManager<IdentityRole>>();
    IdentitySeedData.Initialize(context, userMgr, roleMgr).Wait();
}
app.UseAuthorization();
app.Run();