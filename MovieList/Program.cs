using MovieList.DAL;
using MovieList;
using Microsoft.EntityFrameworkCore;
using MovieList.Domain.Entity.Account;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MovieList.Services;
using MovieList.Hubs;
using Microsoft.AspNetCore.Identity;
using MovieList.Domain.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

//Initialize
builder.Services.InitializeRepositories();
builder.Services.InitializeServices();

builder.Services.AddHttpContextAccessor();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");

var emailConfig = builder.Configuration
        .GetSection("EmailConfiguration")
        .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);

//Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

//Database
builder.Services.AddDbContext<ApplicationDbContext>(option =>
        option.UseSqlServer(connection));

//Identity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(opt =>
{
    opt.SignIn.RequireConfirmedEmail = true;
    opt.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddMvc();
builder.Services.AddControllers();
builder.Services.AddRazorPages();

builder.Services.AddSwaggerGen();

//SignalR
builder.Services.AddSignalR();

//AutoMapper
var config = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AppMappingProfile());
});

builder.Services.AddSingleton(config.CreateMapper());

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
