using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OdontoPrev.Data;
using OdontoPrev.Repositories;
using OdontoPrev.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();

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

// Adicione este middleware para redirecionar URLs com .html
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

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();