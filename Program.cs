using Azure.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(sp =>
{
  var clientId = "";
  var tenantId = "";
  var certificatePath = @"";
  var certificatePassword = "";
  var scopes = new string[]
  { 
    "https://graph.microsoft.com/.default"
  };

  var options = new TokenCredentialOptions
  {
    AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
  };

  var certificate = new X509Certificate2(certificatePath, certificatePassword);
  var clientCertCredential = new ClientCertificateCredential(
    tenantId, clientId, certificate, options);

  return new GraphServiceClient(clientCertCredential, scopes);
});

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
