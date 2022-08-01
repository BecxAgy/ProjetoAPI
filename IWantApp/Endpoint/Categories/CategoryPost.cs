using IWantApp.Domain.Products;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace IWantApp.Endpoint.Categories;

public static class CategoryPost
{
   
    public static string Template => "/categories";
    public static string[] Method => new string[] { HttpMethod.Post.ToString() };

    public static Delegate Handle => Action;

    [Authorize]

    public static IResult Action(CategoryRequest categoryRequest, HttpContext http, ApplicationDbContext context)
    {
        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var category = new Category(categoryRequest.Name,userId,userId);


        if (!category.IsValid)
        {
            //Agroup the keys(name,createdby,editedby), turns them into ToDicionary at the same time it seeks the key to the function 
           
            return Results.ValidationProblem(category.Notifications.ConvertToProblemsDetails());
         }   


        context.Categories.Add(category);
        context.SaveChanges();

        return Results.Created($"/categories/{category.Id}", category.Id);

    }

    
}


