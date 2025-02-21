using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UserManagementAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "UserManagementAPI", Version = "v1" });
});

// Add JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "http://localhost",
            ValidAudience = "http://localhost",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("bXyZ9!@#4QwErTyUiOpAsDfGhJkLzXcVbNm1234567890"))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserManagementAPI v1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the root
    });
}
app.UseHttpsRedirection();

// Use error-handling middleware first
app.UseMiddleware<ErrorHandlingMiddleware>();

// Use authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

// Use logging middleware last
app.UseMiddleware<RequestLoggingMiddleware>();

app.MapControllers();

app.Run();