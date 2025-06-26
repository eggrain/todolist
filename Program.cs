var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

string dbCon = null!;

if (builder.Environment.IsDevelopment())
{
    dbCon = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "Is SQLite connection string in appsettings.json missing?");
}
else if (builder.Environment.IsProduction())
{
    dbCon = builder.Configuration["todolist_DATABASE_PATH"]
        ?? throw new InvalidOperationException(
            "Is todolist_DATABASE_CONNECTION environment variable present?");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(dbCon));

builder.Services.AddIdentity<AppUser, IdentityRole>(
    options => options.SignIn.RequireConfirmedAccount = false)
        .AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();

using IServiceScope scope = app.Services.CreateScope();
AppDbContext db = scope.ServiceProvider
                .GetRequiredService<AppDbContext>();
db.Database.Migrate();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
