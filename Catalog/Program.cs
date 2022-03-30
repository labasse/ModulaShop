using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Catalog.Data;


var builder = WebApplication.CreateBuilder(args);

if(!builder.Environment.IsEnvironment("Test"))
{
    builder.Services.AddDbContext<CatalogContext>(options =>

        options.UseSqlServer(builder.Configuration.GetConnectionString("CatalogContext")));
}

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c => c.IncludeXmlComments(Path.Combine(
        AppContext.BaseDirectory,
        $"{ Assembly.GetExecutingAssembly().GetName().Name }.xml"
    ))
);
builder.Services.AddRouting(sp => sp.LowercaseUrls = true);

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

public partial class Program { }
