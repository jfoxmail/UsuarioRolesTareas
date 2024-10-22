using Azure.Identity;
using DoubleV.UsuarioRolesTareas.Api;
using DoubleV.UsuarioRolesTareas.Application;
using DoubleV.UsuarioRolesTareas.Persintence;

var builder = WebApplication.CreateBuilder(args);

var keyVaultUrl = builder.Configuration["keyVaultUrl"] ?? string.Empty;
if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "local")
{
    string tenantId = Environment.GetEnvironmentVariable("tenantId") ?? string.Empty;
    string clientId = Environment.GetEnvironmentVariable("clientId") ?? string.Empty;
    string clientSecret = Environment.GetEnvironmentVariable("clientSecret") ?? string.Empty;
    var tokenCredentials = new ClientSecretCredential(tenantId, clientId, clientSecret);
    builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUrl), tokenCredentials);
}
else
{
    builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUrl), new DefaultAzureCredential());
}

builder.Services
    .AddWebApi()
    .AddApplication()
    .AddPersistence(builder.Configuration)
    .AddControllers();


builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});


app.UseCors("PermitirTodo");

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
