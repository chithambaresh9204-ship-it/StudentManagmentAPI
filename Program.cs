
using Microsoft.EntityFrameworkCore;
using StudentManagementAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// SQL Server connection
builder.Services.AddDbContext<StudentDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("StudentDBConnection")
    ));

var app = builder.Build();

// Swagger configuration
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
