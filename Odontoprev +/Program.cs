using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OdontoPrev.Data;
using OdontoPrev.Repositories;
using OdontoPrev.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Configuração de Autenticação
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.LogoutPath = "/Logout";
        options.AccessDeniedPath = "/AccessDenied";
        options.Cookie.Name = "OdontoPrevAuth";
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromHours(24);
        options.SlidingExpiration = true;
    });

// Configure DbContext
builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();

// Register services
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();

// Configure Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "OdontoPrev API", Version = "v1" });
});

// Anti-forgery validation
builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "X-CSRF-TOKEN";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "OdontoPrev API v1");
        c.RoutePrefix = "swagger";
    });
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}


app.Use(async (context, next) =>
{
    if (context.Request.Path.Value.EndsWith(".html", StringComparison.OrdinalIgnoreCase))
    {
        var newPath = context.Request.Path.Value.Replace(".html", "");
        context.Response.Redirect(newPath);
        return;
    }
    await next();
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();