using Ocelot.DependencyInjection;
using Ocelot.Middleware;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("configration.json")
               .AddJsonFile($"configration.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables();
builder.Services.AddCors(options => options.AddPolicy("AllowCors", builder =>
{
    builder.AllowAnyOrigin().WithMethods("GET", "PUT", "POST", "DELETE").AllowAnyHeader();
}));
builder.Services.AddOcelot();
builder.Services.AddAuthentication();
var app = builder.Build();
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowCors");
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});
app.UseOcelot().Wait();
app.Run();







