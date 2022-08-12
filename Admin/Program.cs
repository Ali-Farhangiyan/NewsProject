using Application.Interfaces;
using Application.Services.CategoryServices.CategoryMainService;
using Application.Services.CommentServices.CommentMainService;
using Application.Services.NewsServices.NewsMainService;
using Infrastructure.IdentityService;
using Infrastructure.InfrastructureServices.ImageService;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


builder.Services.AddDbContext<DatabaseContext>(op =>
{
    op.UseSqlServer(builder.Configuration["ConnectionString:SqlServerConnection"]);

});


builder.Services.AddTransient<IDatabaseContext, DatabaseContext>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddMyIdentityService(builder.Configuration);







var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();




app.MapRazorPages();

app.Run();
