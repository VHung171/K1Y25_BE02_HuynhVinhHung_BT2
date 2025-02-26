using K1Y25_BE02_HuynhVinhHung_BT2.Data;
using K1Y25_BE02_HuynhVinhHung_BT2.IServices;
using K1Y25_BE02_HuynhVinhHung_BT2.Repositories;
using K1Y25_BE02_HuynhVinhHung_BT2.Services;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using K1Y25_BE02_HuynhVinhHung_BT2.Mapper; 
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens; 
var builder = WebApplication.CreateBuilder(args);

// Lấy chuỗi kết nối từ appsettings.json
var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");

// Đăng ký DbContext sử dụng PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Cấu hình Authentication với JWT
var jwtKey = builder.Configuration["Jwt:Key"];
if (string.IsNullOrEmpty(jwtKey))
{
    throw new Exception("Jwt:Key is missing in appsettings.json");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "BT2 API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Please enter 'Bearer' [space] and then your token",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        { new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            }, new string[] { }
        }
    });
});

builder.Services.AddAuthorization();
builder.Services.AddAutoMapper(typeof(UserMapper).Assembly);

// Đăng ký Repository
builder.Services.AddScoped<UserRepo>();
builder.Services.AddScoped<RoleRepo>();
builder.Services.AddScoped<AllowAccessRepo>();
builder.Services.AddScoped<InternRepo>();

// Đăng ký Service
builder.Services.AddScoped<IInternService, InternService>();
builder.Services.AddScoped<InternService>(); // or AddTransient/AddSingleton as needed
builder.Services.AddScoped<IInternService, InternService>();

builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<IUserService ,UserService>();
builder.Services.AddScoped<IRoleService ,RoleService>();
builder.Services.AddScoped<IAllowAccessService ,AllowAccessService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();// Kích hoạt xác thực JWT
app.UseAuthorization();

app.MapControllers();

app.Run();
