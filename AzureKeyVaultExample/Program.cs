using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

var builder = WebApplication.CreateBuilder(args);

string keyVaultUrl = builder.Configuration["AzurekeyVaultConfig:KVUrl"];
string tenantId = builder.Configuration["AzurekeyVaultConfig:KVTenantId"];
string clientId = builder.Configuration["AzurekeyVaultConfig:KVClientId"];
string clientSecretId = builder.Configuration["AzurekeyVaultConfig:KVClientSecretId"];

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .AddEnvironmentVariables();

var clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecretId);
var secretClient = new SecretClient(new Uri(keyVaultUrl), clientSecretCredential);

// Add services to the container.

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

app.UseAuthorization();

app.MapControllers();

app.Run();
