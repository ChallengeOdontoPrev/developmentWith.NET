using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OdontoPrev.Data;
using OdontoPrev.Repositories;
using OdontoPrev.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure DbContext
builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();

// Register services
builder.Services.AddScoped<IBlogService, BlogService>();

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
        c.RoutePrefix = string.Empty; // Serve o Swagger UI na raiz
    });

    // Redireciona a raiz para o Swagger UI
    app.Use(async (context, next) =>
    {
        if (context.Request.Path.Value == "/")
        {
            context.Response.Redirect("/swagger");
            return;
        }
        await next();
    });
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<BlogDbContext>();
    context.Database.Migrate();
}

app.Run();