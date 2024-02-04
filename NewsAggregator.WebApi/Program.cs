using Microsoft.EntityFrameworkCore;
using NewsAggregator.BusinessLogic.Interfaces;
using NewsAggregator.BusinessLogic.Mappings;
using NewsAggregator.BusinessLogic.Services;
using NewsAggregator.DataAccess.Data;
using NewsAggregator.DataAccess.Entities;
using NewsAggregator.DataAccess.Interfaces;
using NewsAggregator.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<NewsAggregatorContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection")));

builder.Services.AddAutoMapper(typeof(NewsProfile));
builder.Services.AddScoped<IRepository<News>, NewsRepository>();
builder.Services.AddScoped<INewsService, NewsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<NewsAggregatorContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

app.Run();
