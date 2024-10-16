using BulkyWeb.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.InstallServices(configuration: builder.Configuration, typeof(IServiceInstaller).Assembly);

var app = builder.Build();

if (builder.Configuration.GetValue<bool>("USE_INMEMORY_DB"))
	app.Services.ApplyMigrationsToInMemoryDB();

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
	pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
