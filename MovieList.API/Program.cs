using MovieList.DAL;
using MovieList.Hubs;
using MovieList.API.Infrastructure.Extensions;
using MovieList.Core.Services;
using MovieList.Core.Interfaces;
using MovieList.API.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustomServices();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddSwaggerServices();

builder.Services.AddMvc();
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularClient",
       builder => builder
       .WithOrigins("http://localhost:4200")
       .WithOrigins("https://maksbrat.github.io")
       .AllowAnyMethod()
       .AllowAnyHeader());
});

var app = builder.Build();

await AppInitializer.InitializeAsync(app.Services);

app.ConfigureCustomExceptionMiddleware();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors("AllowAngularClient");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MovieList API V1");
});

app.MapControllers();

app.MapHub<MovieListHub>("/hub");

app.Run();
