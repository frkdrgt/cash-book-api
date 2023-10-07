using AutoMapper;
using CashBook.Data;
using CashBook.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
}); ;

//TODO: Refactor
var config = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<User, UserUpdateRequestDto>().ReverseMap();

    cfg.CreateMap<BankTransaction, BankTransactionCreateRequestDto>().ReverseMap();
    cfg.CreateMap<BankTransaction, BankTransactionGetDto>().ReverseMap();
    cfg.CreateMap<BankTransaction, BankTransactionUpdateRequestDto>().ReverseMap();

    cfg.CreateMap<Product, ProductCreateDto>().ReverseMap();
    cfg.CreateMap<Product, ProductUpdateDto>().ReverseMap();
    cfg.CreateMap<Product, ProductGetDto>().ReverseMap();

    cfg.CreateMap<ProductCategory, ProductCategoryCreateDto>().ReverseMap();
    cfg.CreateMap<ProductCategory, ProductCategoryUpdateDto>().ReverseMap();
    cfg.CreateMap<ProductCategory, ProductCategoryDto>().ReverseMap();


    cfg.CreateMap<Warehouse, WarehouseGetDto>().ReverseMap();
    cfg.CreateMap<Warehouse, WarehouseCreateDto>().ReverseMap();
    cfg.CreateMap<Warehouse, WarehouseUpdateDto>().ReverseMap();

});

IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddDbContext<CashBookDbContext>(options =>
            options.UseSqlServer(
                  builder.Configuration.GetConnectionString("DefaultConnection"), y => y.MigrationsAssembly("CashBook.Data")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

builder.Services.AddHttpContextAccessor();
builder.Services.AddRepository();

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;

});

builder.Services.Configure<BrotliCompressionProviderOptions>(o =>
{
    o.Level = System.IO.Compression.CompressionLevel.Fastest;
});

builder.Services.AddCors();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "CashBook API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });

});


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtToken:Issuer"],
        ValidAudience = builder.Configuration["JwtToken:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtToken:SecretKey"])),
    };
});
builder.Services.AddAuthorization();
builder.Services.AddSwaggerGenNewtonsoftSupport();

var app = builder.Build();
app.UseResponseCompression();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseRouting();
app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
