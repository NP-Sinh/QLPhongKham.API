using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QLPhongKham.API.Models.Entities;
using QLPhongKham.API.Models.Map;
using QLPhongKham.API.Services;
using QLPhongKham.API.Services.AuthServices;
using QLPhongKham.API.Services.ConvertDBToJsonServices;
using QLPhongKham.API.Services.MemoryCaching;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddDbContext<PhongKhamDBContext>(c =>
        c.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

// Newtonsoft.Json config
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

// Memory Cache
builder.Services.AddMemoryCache(options =>
{
    options.SizeLimit = 1024; 
    options.CompactionPercentage = 0.25; 
    options.ExpirationScanFrequency = TimeSpan.FromMinutes(5);
});
// JWT config
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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


// Add services to the container.
builder.Services.AddScoped<JwtServices>();
builder.Services.AddScoped<IAuthServices, AuthServices>();
builder.Services.AddScoped<IConvertDBToJsonServices, ConvertDBToJsonServices>();
builder.Services.AddScoped<IVaiTroServices, VaiTroServices>();
builder.Services.AddScoped<IBenhNhanServices, BenhNhanServices>();
builder.Services.AddScoped<IChuyenKhoaServices, ChuyenKhoaServices>();
builder.Services.AddScoped<IBacSiServices, BacSiServices>();
builder.Services.AddScoped<INguoiDungServices, NguoiDungServices>();
builder.Services.AddScoped<IPhongKhamServices, PhongKhamServices>();
builder.Services.AddScoped<IThuocServices, ThuocServices>();
builder.Services.AddScoped<ILichHenServices, LichHenServices>();
builder.Services.AddScoped<IPhhieuKhamBenhServices, PhieuKhamBenhServices>();
builder.Services.AddScoped<IDonThuocServices, DonThuocServices>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddAuthorization();

builder.Services.AddMemoryCache();
builder.Services.AddScoped<IMemoryCaching, MemoryCaching>();
builder.Services.AddResponseCaching();

// CORS cho Angular
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy => policy
            .WithOrigins("http://localhost:4200") // địa chỉ Angular dev server
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

app.UseResponseCaching();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
