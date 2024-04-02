using MovieList.DAL;
using MovieList;
using MovieList.Hubs;
using MovieList.API.Infrastructure.Extensions;

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

var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
DbInitializer.Initialize(context);

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

app.MapHub<ChatHub>("/chatHub");

app.Run();
