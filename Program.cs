using BackEnd_SocialE.Learning.Domain.Repositories;
using BackEnd_SocialE.Learning.Domain.Services;
using BackEnd_SocialE.Learning.Mapping;
using BackEnd_SocialE.Learning.Persistence.Repositories;
using BackEnd_SocialE.Learning.Services;
using BackEnd_SocialE.Security.Authorization.Handlers.Implementations;
using BackEnd_SocialE.Security.Authorization.Handlers.Interfaces;
using BackEnd_SocialE.Security.Authorization.MiddleWare;
using BackEnd_SocialE.Security.Authorization.Settings;
using BackEnd_SocialE.Security.Domain.Repositories;
using BackEnd_SocialE.Security.Persistence.Repositories;
using BackEnd_SocialE.Security.Services;
using BackEnd_SocialE.Shared.Persistence.Contexts;
using BackEnd_SocialE.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add CORS
builder.Services.AddCors();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo {
        Version = "v1",
        Title = "BackEnd SocialE",
        Description = "BackEnd SocialE, Restful Api",
        TermsOfService = new Uri("https://social-e.com/tos"),
        Contact = new OpenApiContact
        {
            Name = "OnService",
            Url = new Uri("https://onservice.studio")
        },
        License = new OpenApiLicense
        {
            Name = "SocialE Resources License",
            Url = new Uri("https://social-e.com/license")
        }
    });
    options.EnableAnnotations();
    options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme."
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        { new OpenApiSecurityScheme {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
            }, Array.Empty<string>()
        } 
    });
});

// Add Database Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseMySQL(connectionString)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors());

// Add lowercase routes
builder.Services.AddRouting(options => options.LowercaseUrls = true);
// AppSettings Configuration
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
// Security Injection Configuration
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// Security Injection Configuration
builder.Services.AddScoped<IJwtHandler, JwtHandler>();
// AutoMapper Configuration
builder.Services.AddAutoMapper(
    typeof(ModelToResourceProfile),
    typeof(BackEnd_SocialE.Security.Mapping.ModelToResourceProfile),
    typeof(ResourceToModelProfile),
    typeof(BackEnd_SocialE.Security.Mapping.ResourceToModelProfile));

var app = builder.Build();

// Validation for ensuring Database Objects are created
using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<AppDbContext>()) {
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => {
        options.SwaggerEndpoint("v1/swagger.json", "v1");
        options.RoutePrefix = "swagger";
    });
}
// Configure CORS
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// Configure Error Handler Middleware

app.UseMiddleware<ErrorHandlerMiddleware>();

// Configure JWT Handling

app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();