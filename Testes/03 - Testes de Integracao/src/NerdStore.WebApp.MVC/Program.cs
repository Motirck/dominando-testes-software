using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NerdStore.Catalogo.Application.AutoMapper;
using NerdStore.Catalogo.Data;
using NerdStore.Vendas.Data;
using NerdStore.WebApp.MVC.Data;
using NerdStore.WebApp.MVC.Security;
using NerdStore.WebApp.MVC.Setup;

var builder = WebApplication.CreateBuilder(args);

string apiName;
string apiDescription;
string apiVersion;

// Add services to the container.
var configuration = builder.Configuration;

apiName = configuration["ApiName"];
apiDescription = configuration["ApiDescription"];
apiVersion = configuration["ApiVersion"];

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<CatalogoContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<VendasContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentityCore<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(typeof(DomainToViewModelMappingProfile), typeof(ViewModelToDomainMappingProfile));

builder.Services.AddMediatR(typeof(Program));

builder.Services.RegisterServices();

builder.Services.AddCors(setup =>
{
    setup.AddPolicy(AllowWhitelistCorsPolicy.Name, AllowWhitelistCorsPolicy.Get(configuration["CorsWhitelist"].Split(',')));
});

builder.Services.AddSwaggerGen(options =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Description = "Insira o token JWT desta maneira: Bearer {seu token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    };
    options.AddSecurityDefinition("Bearer", jwtSecurityScheme);

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
        
    options.SwaggerDoc(apiVersion, new OpenApiInfo { Title = apiName, Version = apiVersion, Description = apiDescription });

    /* Due to the need to put a description in each property of the "Request / Dto" classes used in the [FromQuery] of the endpoints, 
     * the implementation below was necessary, where through forEach the inclusion of all XML files is performed. 
     * (Microsoft's official documentation only explains how to do it for one XML file) */
    List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
    xmlFiles.ForEach(xmlFile => options.IncludeXmlComments(xmlFile));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Swagger
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint($"{apiVersion}/swagger.json", $"{apiName} {apiVersion}");
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseDeveloperExceptionPage();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Vitrine}/{action=Index}/{id?}");

app.Run();
