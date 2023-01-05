using FreeBilling.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddTransient<IEmailService, FakeEmailService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
}

// app.MapGet("/", () => "Hello World!");

//app.UseDefaultFiles();
app.UseStaticFiles();

app.MapRazorPages();

app.Run();
