using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using InventarioService;
using InventarioService.Middelwares;
using InventarioService.Services.Interface;
using InventarioService.Services.Implementacion;
using Repositorio_Inventario.UnitOfWork;
using Servicios_Inventario.Service.Interface;
using Servicios_Inventario.Service.Implementacion;
using Repositorio_Inventario;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var connectionString = builder.Configuration.GetConnectionString("DefaultConexion");
builder.Services.AddDbContext<SqlDbContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

static bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters) => expires != null && expires > DateTime.Now;


builder.Services.AddSwaggerGen(c =>
{
    c.CustomSchemaIds(type => type.ToString());
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "JK Smart Data ALMACEN Api", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."

    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var tokenKey = builder.Configuration.GetValue<string>("TokenKey");
var key = Encoding.ASCII.GetBytes(tokenKey);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        LifetimeValidator = LifetimeValidator,
        TokenDecryptionKey = new SymmetricSecurityKey(key),
    };
});

List<CorsOrigin> origins = new List<CorsOrigin>();
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Configuration.GetSection("cors:origins").Bind(origins);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
    config =>
    {
        foreach (var o in origins)
        {
            Console.WriteLine(o.uri);
            config.WithOrigins(o.uri).AllowAnyMethod().AllowAnyHeader();
        }
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();


builder.Services.AddSingleton<ICustomAuthenticationManagerService>(new CustomAuthenticationManagerServiceImpl(tokenKey));
builder.Services.AddTransient<GlobalExceptionsHandlingMiddelware>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//implemantacion de servicios
builder.Services.AddTransient(typeof(IEmpresaService), typeof(EmpresaService));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(MyAllowSpecificOrigins);

app.UseMiddleware<GlobalExceptionsHandlingMiddelware>();

app.MapControllers();

app.Run();
