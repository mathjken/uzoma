using Microsoft.EntityFrameworkCore;
using Uzoma.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddDbContext<UzomaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Seed database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<UzomaDbContext>();
    dbContext.Database.EnsureCreated();
    DbInitializer.Seed(dbContext);
}

app.Run();
