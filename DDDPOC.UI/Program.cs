using DDDPOC.Application.Commands.Customer;
using DDDPOC.Infrastructure.EventBus;
using DDDPOC.Infrastructure.Infrastructure.Data;
using DDDPOC.Infrastructure.MessageBroker;
using DDDPOC.Infrastructure.Repositories;
using DDDPOC.UI.Authentication;
using DDDPOC.UI.Keycloack;
using MassTransit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<MessageBrokerSettings>(
    builder.Configuration.GetSection("MessageBroker"));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();
    busConfigurator.AddConsumer<CustomerCreatedEventConsumer>();
    busConfigurator.UsingRabbitMq((context, configurator)=>
    {
        MessageBrokerSettings settings = context.GetRequiredService<MessageBrokerSettings>();
        configurator.Host(new Uri(settings.Host), h =>
        {
            h.Username(settings.Username);
            h.Password(settings.Password);
        });
    });
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(AppDomain.CurrentDomain.Load("DDDPOC.Application")));
builder.Services.AddControllers();
builder.Services.AddTransient<IClaimsTransformation, ClaimsTransformer>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.Load("DDDPOC.Application"));
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "test",
    builder =>
    {
        builder.WithOrigins("*").WithHeaders().AllowAnyHeader();
    });
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "E Shop", Version = "v1" });
    //First we define the security scheme
    c.AddSecurityDefinition("Bearer", //Name the security scheme
        new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme.",
            Type = SecuritySchemeType.Http, //We set the scheme type to http since we're using bearer authentication
            Scheme = JwtBearerDefaults.AuthenticationScheme //The name of the HTTP Authorization scheme to be used in the Authorization header. In this case "bearer".
        });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Id = JwtBearerDefaults.AuthenticationScheme, //The name of the previously defined security scheme.
                                Type = ReferenceType.SecurityScheme
                            }
                        },new List<string>()
                    }
                });
});
builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
builder.Services.AddTransient<IEventBus, EventBus>();
var keycloackConfig = builder.Configuration.GetSection("KeycloackConfig").Get<KeycloackConfig>();
builder.Services.ConfigureJWTWithKeycloack(builder.Environment.IsDevelopment(), keycloackConfig);
builder.Services.TryAddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.TryAddSingleton<IClaimsTransformation, NoopClaimsTransformation>();
builder.Services.TryAddScoped<IAuthenticationHandlerProvider, AuthenticationHandlerProvider>();
builder.Services.TryAddSingleton<IAuthenticationSchemeProvider, AuthenticationSchemeProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("test");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
