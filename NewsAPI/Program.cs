using System.Text;
using APINews.Services;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NewsAPI.Infra;
using NewsAPI.Infra.Interfaces;
using NewsAPI.Mappers;
using NewsAPI.Services;
using NewsAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Input your Bearer token to access this API",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference{Type = ReferenceType.SecurityScheme, Id = "bearer"}
            },
            Array.Empty<string>()
        }
    });
});

#region [Database]
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings)));
builder.Services.AddSingleton<IDatabaseSettings>(sp =>
    sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<DatabaseSettings>>().Value);
#endregion

#region [HealthChecks]
builder.Services.AddHealthChecks()
    .AddRedis(builder.Configuration.GetSection("Redis:ConnectionString").Value, tags: new string[] {"db", "data"})
    .AddMongoDb(builder.Configuration.GetSection("DatabaseSettings:ConnectionString").Value + "/" +
                builder.Configuration.GetSection("DatabaseSettings:db_portal").Value,
        name: "mongodb", tags: new string[] {"db", "data"});

builder.Services.AddHealthChecksUI(opt =>
{
    opt.SetEvaluationTimeInSeconds(15);
    opt.MaximumHistoryEntriesPerEndpoint(60);
    opt.SetApiMaxActiveRequests(1);
    opt.AddHealthCheckEndpoint("default api", "/health");
}).AddInMemoryStorage();
#endregion
            
#region [DI]
builder.Services.AddSingleton(typeof(IMongoRepository<>), typeof(MongoRepository<>));
builder.Services.AddSingleton<NewService>();
builder.Services.AddSingleton<VideoService>();
builder.Services.AddSingleton<GalleryService>();
builder.Services.AddTransient<UploadService>();

// builder.Services.AddSingleton<IMemoryCache, MemoryCache>();
// builder.Services.AddSingleton<ICacheService, CacheMemoryService>();
builder.Services.AddSingleton<ICacheService, CacheRedisService>();
#endregion
            
#region [AutoMapper]
builder.Services.AddAutoMapper(typeof(EntityToViewModelMapping));
#endregion

#region [Cors]
builder.Services.AddCors();
#endregion

#region [Jwt]
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                builder.Configuration.GetSection("tokenManagement:secret").Value ?? string.Empty)),
            ValidateIssuer = false,
            ValidAudience = "Any"
        };
    });
#endregion

#region [Cache]
builder.Services.AddDistributedRedisCache(options =>
{
    options.Configuration =
        builder.Configuration.GetSection("Redis:ConnectionString").Value;
});
#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region [HealthChecks]
app.UseHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
}).UseHealthChecksUI(h => h.UIPath = "/health-ui");
#endregion

#region [Cors]

app.UseCors(c => c
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);
#endregion

#region [StaticFiles]

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Medias")),
    RequestPath = "/medias"
});
#endregion

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();