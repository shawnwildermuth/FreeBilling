using FreeBilling.Data;
using FreeBilling.Data.Entities;
using FreeBilling.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

{ // services
  builder.Services.AddRazorPages();
  builder.Services.AddControllers();
  builder.Services.AddTransient<IEmailService, FakeEmailService>();

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
    .AddJwtBearer();

  //builder.Services.AddSpaStaticFiles(cfg => cfg.RootPath = "client") ;
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

  app.UseAuthorization();
  app.UseRouting();

  //app.UseDefaultFiles();
  app.UseStaticFiles();
  app.UseEndpoints(cfg =>
  {

    cfg.MapRazorPages();
    cfg.MapControllers();

    //app.UseSpaStaticFiles();

  });

  if (app.Environment.IsDevelopment())
  {
    app.UseSpa(cfg =>
    {
      cfg.Options.DevServerPort = 5000;
      cfg.Options.SourcePath = "client";
      cfg.UseProxyToSpaDevelopmentServer("http://localhost:5000");
    });
  }

}

app.Run();
