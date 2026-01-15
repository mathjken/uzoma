using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Uzoma.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// --------------------
// Controllers & JSON Formatting (FIXES THE DISAPPEARING ISSUE)
// --------------------
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Forces camelCase (e.g., imageUrl instead of ImageUrl) to match your frontend
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        // Prevents infinite loops if models reference each other
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// --------------------
// Database (PostgreSQL)
// --------------------
string? databaseUrl = builder.Configuration["DATABASE_URL"];
string connectionString;

if (!string.IsNullOrEmpty(databaseUrl))
{
    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':');

    connectionString =
        $"Host={uri.Host};" +
        $"Port={uri.Port};" +
        $"Database={uri.AbsolutePath.TrimStart('/')};" +
        $"Username={userInfo[0]};" +
        $"Password={userInfo[1]};" +
        $"SSL Mode=Require;Trust Server Certificate=true";
}
else
{
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
}

builder.Services.AddDbContext<UzomaDbContext>(options =>
    options.UseNpgsql(connectionString)
);

// --------------------
// Swagger
// --------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --------------------
// CORS (ALLOWS YOUR LOCALHOST TO ACCESS HEROKU)
// --------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// --------------------
// JWT Authentication
// --------------------
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            )
        };
    });

var app = builder.Build();

// --------------------
// Middleware Order (CRITICAL)
// --------------------
app.UseRouting();

// CORS must be here, after Routing and before Authentication
app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

// --------------------
// Swagger for all environments
// --------------------
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Uzoma API V1");
});

// --------------------
// Root endpoint
// --------------------
app.MapGet("/", () => "Uzoma API is running!");

// --------------------
// Map Controllers
// --------------------
app.MapControllers();

// --------------------
// Database migrations & seeding
// --------------------
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<UzomaDbContext>();

    try
    {
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Migration failed: {ex.Message}");
    }

    // This will run your fixed links and categories
    DbInitializer.Initialize(context);
}

app.Run();