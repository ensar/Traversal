using BusinessLayer.Container;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Razor;
using Traversal.CQRS.Handlers.DestinationHandlers;
using Traversal.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<GetAllDestinationQueryHandler>();
builder.Services.AddScoped<GetDestinationByIDQueryHandler>();
builder.Services.AddScoped<CreateDestinationCommandHandler>();
builder.Services.AddScoped<RemoveDestinationCommandHandler>();
builder.Services.AddScoped<UpdateDestinationCommandHandler>();

builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddLogging(x =>
{
    x.ClearProviders();
    x.SetMinimumLevel(LogLevel.Debug);
    x.AddDebug();
});
builder.Services.AddDbContext<Context>();
builder.Services.AddIdentity<AppUser,AppRole>().AddEntityFrameworkStores<Context>()
    .AddErrorDescriber<CustomIdentityValidator>()
    .AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider)
    .AddEntityFrameworkStores<Context>();
builder.Services.ContainerDependencies();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.CustomerValidator();
builder.Services.AddHttpClient();

builder.Services.AddControllersWithViews().AddFluentValidation();

builder.Services.AddLocalization(opt =>
{
    opt.ResourcesPath = "Resources";
});

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new("tr-TR");
    var cultures = new[] { "en", "fr", "es", "tr", "de" };
    options.AddSupportedCultures(cultures);
    options.AddSupportedUICultures(cultures);

    options.FallBackToParentUICultures = true;
    options.RequestCultureProviders.Clear();
});

builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});
builder.Services.AddMvc();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/User/Signin";
});
builder.Logging.AddFile($"{Directory.GetCurrentDirectory()}\\Logs\\Log1.txt");
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/ErrorPage/Error404","?code={0}");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.UseRequestLocalization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
     name: "areas",
     pattern: "{area:exists}/{controller=Message}/{action=Index}/{id?}"
);



app.Run();
