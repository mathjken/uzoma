using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using uzoma_api.Data;

var builder = WebApplication.CreateBuilder(args);

// ------------------------
// Add services
// ------------------------

// Add controllers
builder.Services.AddControllers();

// Add Swagger for testing (optional)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ------------------------
// CORS
// ------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173") // React dev server
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// ------------------------
// Database
// ------------------------
var databaseUrl = builder.Configuration["DATABASE_URL"];
if (string.IsNullOrEmpty(databaseUrl))
{
    Console.WriteLine("DATABASE_URL is not set!");
}

var connectionString = databaseUrl != null
    ? new Npgsql.NpgsqlConnectionStringBuilder(databaseUrl) { 
        // Heroku requires SSL
        SslMode = SslMode.Require,
        TrustServerCertificate = true
    }.ToString()
    : builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// ------------------------
// JWT Authentication
// ------------------------
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

if (string.IsNullOrEmpty(jwtKey))
    Console.WriteLine("JWT Key not set in configuration!");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

var app = builder.Build();

// ------------------------
// Apply Migrations & Seed
// ------------------------
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try
    {
        context.Database.Migrate();
        DbInitializer.Initialize(context);
        Console.WriteLine("Database migrated and seeded successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database migration/seeding failed: {ex}");
    }
}

// ------------------------
// Middleware
// ------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

// CORS must come before Authentication & Authorization
app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// ------------------------
// Heroku port binding
// ------------------------
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Urls.Add($"http://*:{port}");

app.Run();
