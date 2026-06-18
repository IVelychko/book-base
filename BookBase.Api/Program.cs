using BookBase.Api.Extensions;
using BookBase.Api.Middlewares;
using BookBase.Application.Extensions;
using BookBase.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

const string corsPolicyName = "BaseCorsPolicy";

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddOpenApi();

builder.Services.AddCorsPolicy(corsPolicyName, builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();

builder.Services.AddJwtBearerAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseCors(corsPolicyName);

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
