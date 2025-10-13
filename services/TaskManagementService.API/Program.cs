using TaskManagementService.Application; // Bu using'i ekle
using TaskManagementService.Application.Interfaces; // Bu using'i ekle
using TaskManagementService.API.Services; // Bu using'i ekle
using TaskManagementService.Persistence; // Bu using'i ekle
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TaskManagementService.API.Middleware;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Katmanlarımızın servis kayıt metotlarını çağırıyoruz
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);

// ICurrentUserService için somut sınıfı ve HttpContextAccessor'ı kaydediyoruz
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

// --- BU KOD BLOĞUNU EKLE ---
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Token'ı üreten AuthService ile aynı ayarları kullanmalıyız.
        // Şimdilik bu bilgileri doğrudan yazıyoruz.
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "BeeFocusAuthService",
            ValidAudience = "BeeFocusClients",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("BU-BENIM-COK-GUCLU-VE-TAHMIN-EDILEMEZ-GIZLI-ANAHTARIM-12345"))
        };
    });
// --- KOD BLOĞUNUN SONU ---

builder.Services.AddControllers();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Lütfen 'Bearer ' ve ardından token'ı girin",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});
var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();