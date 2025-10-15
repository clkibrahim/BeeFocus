using System.Text;
using FocusTimerService.Application;
using FocusTimerService.Application.Interfaces;
using FocusTimerService.API.Middleware;
using FocusTimerService.API.Services;
using FocusTimerService.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// --- Servis Kayıtları (Dependency Injection) ---

// Katmanlarımızın servis kayıt metotlarını çağırıyoruz
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);

// Mevcut kullanıcıyı okuyacak olan servisi kaydediyoruz
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

// JWT Kimlik Doğrulama ayarlarını ekliyoruz (TaskManagementService ile aynı)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
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

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// Swagger'a "Authorize" butonu ekleyen ayarlar
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

// --- HTTP İstek Boru Hattı (Middleware) ---
app.UseMiddleware<ErrorHandlingMiddleware>(); 

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Sıralama önemli: Önce kimlik doğrula, sonra yetkilendir
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();