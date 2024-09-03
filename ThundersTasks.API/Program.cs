using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using ThundersTasks.Application.Services.Tasks;
using ThundersTasks.Core.Models;
using ThundersTasks.Infrastructure.Data;
using ThundersTasks.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

var configurationBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);

IConfiguration _configuration = configurationBuilder.Build();
var stringConexaoBanco = _configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(stringConexaoBanco));

builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddScoped<IRepository<TaskModel>, Repository<TaskModel>>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
