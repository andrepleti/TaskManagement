using Microsoft.EntityFrameworkCore;
using TaskManagement.API;
using TaskManagement.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularOrigins",
    builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers()
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

builder.Services.AddDbContext<DataBaseContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("MySql"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySql")), x => x.MigrationsAssembly("TaskManagement.Infrastructure"));
});

builder.Services.AddDependencyInjection();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

using var serviceScope = app.Services
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();
serviceScope.ServiceProvider.GetService<DataBaseContext>()!.Database.Migrate();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();