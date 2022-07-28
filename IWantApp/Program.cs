using IWantApp.Endpoint.Categories;
using IWantApp.Endpoint.Employees;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration["ConnectionStrings:iWantDb"]);
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
    options.Password.RequireLowercase = false;
}).AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddScoped<QueryAllUsersWithClaimName>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapMethods(CategoryGet.Template, CategoryGet.Method, CategoryGet.Handle);
app.MapMethods(CategoryPut.Template, CategoryPut.Method, CategoryPut.Handle);
app.MapMethods(CategoryPost.Template, CategoryPost.Method, CategoryPost.Handle);
app.MapMethods(EmployeeGet.Template, EmployeeGet.Method, EmployeeGet.Handle);




app.Run();

