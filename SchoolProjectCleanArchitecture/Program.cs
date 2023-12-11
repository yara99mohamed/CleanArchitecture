using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SchoolProject.Core;
using SchoolProject.Core.Middlewares;
using SchoolProject.Infrastructure;
using SchoolProject.Infrastructure.Context;
using SchoolProject.Service;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("dbcontext"));
});

#region Dependency Injection
builder.Services.AddInfrastructureDependencies()
    .AddServiceDependencies()
    .AddCoreDependencies()
    .AddServiceRegisteration(builder.Configuration);
#endregion

#region Localization
builder.Services.AddControllersWithViews();
builder.Services.AddLocalization(opt =>
{
    opt.ResourcesPath = "";
});
builder.Services.Configure<RequestLocalizationOptions>(opt =>
{
    List<CultureInfo> supportedCultures = new List<CultureInfo>
    {
        CultureInfo.GetCultureInfo("en-US") ,
        CultureInfo.GetCultureInfo("ar-EG")
    };
    opt.DefaultRequestCulture = new RequestCulture("ar-EG");
    opt.SupportedCultures = supportedCultures;
    opt.SupportedUICultures = supportedCultures;
});
#endregion


#region Allowcors
builder.Services.AddCors(options =>
    {
        options.AddPolicy(
            name: "_cors",
            policy =>
            {
                policy.AllowAnyOrigin();
                policy.AllowAnyMethod();
                policy.AllowAnyHeader();
                //policy.AllowCredentials();
            });
    });
#endregion


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();

}

#region Localization MiddleWare 
var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);
#endregion
app.UseMiddleware<MiddlewareErrorHandler>();
app.UseHttpsRedirection();

app.UseCors("_cors");

//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();