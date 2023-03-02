using Business.Services;
using DataAccess.Contexts;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);





#region Localization

List<CultureInfo> cultures = new List<CultureInfo>()
{
	new CultureInfo("en-US") 
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
	options.DefaultRequestCulture = new RequestCulture(cultures.FirstOrDefault().Name);
	options.SupportedCultures = cultures;
	options.SupportedUICultures = cultures;
});
#endregion


// Add services to the container.
builder.Services.AddControllersWithViews();


#region Authentication
builder.Services
	.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	

	.AddCookie(config =>
	

	{
		config.LoginPath = "/Account/Users/Login";
		config.AccessDeniedPath = "/Account/Users/AccessDenied";
		config.ExpireTimeSpan = TimeSpan.FromMinutes(30);
		config.SlidingExpiration = true;

	});
#endregion

#region AppSettings
var section = builder.Configuration.GetSection(nameof(AppSettings));
section.Bind(new AppSettings());
#endregion

#region Session
builder.Services.AddSession(config =>
{
	config.IdleTimeout = TimeSpan.FromMinutes(30);
});
#endregion

#region IoC Container (Inversion of Control)

var connectionString = builder.Configuration.GetConnectionString("LibraryDb");
builder.Services.AddDbContext<LibraryContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<BookRepoBase, BookRepo>(); 
builder.Services.AddScoped<CategoryRepoBase, CategoryRepo>();
builder.Services.AddScoped<WriterRepoBase, WriterRepo>();
builder.Services.AddScoped<CountryRepoBase, CountryRepo>();
builder.Services.AddScoped<CityRepoBase, CityRepo>();
builder.Services.AddScoped<UserRepoBase, UserRepo>();




builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IWriterService, WriterService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAccountService, AccountService>();
#endregion

var app = builder.Build();

#region Localization
app.UseRequestLocalization(new RequestLocalizationOptions()
{
	DefaultRequestCulture = new RequestCulture(cultures.FirstOrDefault().Name),
	SupportedCultures = cultures,
	SupportedUICultures = cultures,
});
#endregion

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

#region Authentication
app.UseAuthentication(); 
#endregion

app.UseAuthorization();

#region Session
app.UseSession();
#endregion


app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
	  name: "areas",
	  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
	);
});


app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
