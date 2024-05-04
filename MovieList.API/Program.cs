using MovieList.Hubs;
using MovieList.API.Infrastructure.Extensions;
using MovieList.API.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustomServices();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddSwaggerServices();

builder.Services.AddMvc();
builder.Services.AddControllers();

var app = builder.Build();

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

await DbInitializer.Seed(app);

app.Run();
