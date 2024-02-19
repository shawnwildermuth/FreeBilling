using FluentValidation;
using FreeBilling.Data;
using FreeBilling.Data.Entities;
using FreeBilling.Services;
using FreeBilling.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WilderMinds.MinimalApiDiscovery;

var builder = WebApplication.CreateBuilder(args);

{ // services
  builder.Services.AddRazorPages();
  builder.Services.AddTransient<IEmailService, FakeEmailService>();
  builder.Services.AddApis();
  builder.Services.AddValidatorsFromAssemblyContaining<IValidator>();

  builder.Services.AddDbContext<BillingContext>(opt =>
  {
    var connString = builder.Configuration.GetConnectionString("BillingDb");
    opt.UseSqlServer(connString);
  });

  builder.Services.AddIdentityCore<Employee>(cfg => cfg.User.RequireUniqueEmail = true)
     .AddEntityFrameworkStores<BillingContext>();

  builder.Services.AddTransient<IBillingRepository, BillingRepository>();

  builder.Services.AddAutoMapper(cfg => cfg.AddProfile<BillingMaps>());

  builder.Services.AddAuthentication()
    //.AddCookie()
    .AddJwtBearer();
}

var app = builder.Build();

{ // Middleware
  if (app.Environment.IsDevelopment())
  {
    app.UseDeveloperExceptionPage();
  }

  //app.MapGet("/api/customers", async (IBillingRepository repo) => {
  //  return Results.Ok(await repo.GetAllCustomers());
  //});

  //app.MapPost("/api/customers", async (Customer model, IBillingRepository repo) => {
  //  repo.Add(model);
  //  if (await repo.SaveChanges())
  //  {
  //    return Results.Created($"/api/customers/{model.Id}", model);
  //  }
  //  return Results.BadRequest("Failed to add Customer");
  //});

  app.UseRouting();
  app.UseAuthorization();

  //app.UseDefaultFiles();
  app.UseStaticFiles();

  app.MapRazorPages();
  app.MapApis();

  // Only fallback on the user page
  app.MapFallbackToPage("/User/{*path}", "/User"); 
}

app.Run();
