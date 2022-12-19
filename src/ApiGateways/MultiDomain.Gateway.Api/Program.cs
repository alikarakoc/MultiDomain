using Ocelot.DependencyInjection;
using Ocelot.Middleware;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Configuration.AddJsonFile($"configration.{builder.Environment.EnvironmentName.ToLower()}.json");
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
app.UseWebSockets();
app.UseOcelot().Wait();
app.Run();







