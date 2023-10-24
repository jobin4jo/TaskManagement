using Microsoft.EntityFrameworkCore;
using TaskManagement.DataContracts.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


///db Configuration :
///
builder.Services.AddDbContext<DBContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("Application_Database")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
