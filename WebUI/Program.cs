using Application.Interfaces;
using Application.Services.CategoryServices.CategoryMainService;
using Application.Services.CommentServices.CommentMainService;
using Application.Services.HomeServices.HomeMainService;
using Application.Services.NewsServices.NewsMainService;
using Infrastructure.IdentityService;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();




builder.Services.AddDbContext<DatabaseContext>(op =>
{
    op.UseSqlServer(builder.Configuration["ConnectionString:SqlServerConnection"]);

});


builder.Services.AddTransient<IDatabaseContext, DatabaseContext>();
builder.Services.AddTransient<INewsService, NewsService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<ICommentService, CommentService>();
builder.Services.AddTransient<IHomeService, HomeService>();


builder.Services.AddMyIdentityService(builder.Configuration);









var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute("detail","news/{Slug}",new { Controller = "News", Action = "Details" });
app.MapControllerRoute("category","category/{Slug}",new { Controller = "News", Action = "Index" });



app.Run();
