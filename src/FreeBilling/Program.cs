using FreeBilling.Data;
using FreeBilling.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddTransient<IEmailService, FakeEmailService>();

builder.Services.AddDbContext<BillingContext>(opt =>
{
  var connString = builder.Configuration.GetConnectionString("BillingDb");
  opt.UseSqlServer(connString);
}); 

builder.Services.AddTransient<IBillingRepository, BillingRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
}

app.MapGet("/api/", () => "Hello World!");

//app.UseDefaultFiles();
app.UseStaticFiles();

app.MapRazorPages();

app.Run();
