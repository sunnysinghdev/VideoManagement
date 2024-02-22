using Domain.Repository;
using Domain.Services;
using Infrastructure.Database;
using Infrastructure.Logging;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
builder.Host.ConfigureSerilog("log.txt");

// Add Database
builder.Services.AddDbContext<AppDbContext>(option => option.UseInMemoryDatabase("VideoDatabase"), ServiceLifetime.Singleton);
builder.Services.AddSingleton<IVideoRepository, VideoRepository>();
builder.Services.AddScoped<IVideoService, VideoService>();
var folder = builder.Configuration["UploadFolder"];
string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), folder ?? throw new ArgumentNullException(folder));
builder.Services.AddScoped<IFileService>(ctx => new FileService(uploadFolder));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApiVersioning().AddMvc();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
