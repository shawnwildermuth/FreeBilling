using FluentValidation;
using FreeBilling.Data;
using FreeBilling.Services;
using Microsoft.EntityFrameworkCore;
using WilderMinds.MinimalApiDiscovery;

var builder = WebApplication.CreateBuilder(args);

// services
builder.Services.AddTransient<IEmailService, FakeEmailService>();
builder.Services.AddValidatorsFromAssemblyContaining<IValidator>();

builder.Services.AddDbContext<BillingContext>(opt =>
{
  var connString = builder.Configuration.GetConnectionString("BillingDb");
  opt.UseSqlServer(connString);
});

builder.Services.AddTransient<IBillingRepository, BillingRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(cfg => cfg.AddDefaultPolicy(config =>
{
  config.AllowAnyHeader();
  config.AllowAnyMethod();
  config.AllowAnyOrigin();
}));

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseCors();

//app.UseAuthorization();

app.MapApis();

app.Run();
