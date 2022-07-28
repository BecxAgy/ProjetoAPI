using IWantApp.Domain.Products;
using IWantApp.Infra.Data;

namespace IWantApp.Endpoint.Categories;

public static class CategoryPost
{
   
    public static string Template => "/categories";
    public static string[] Method => new string[] { HttpMethod.Post.ToString() };

    public static Delegate Handle => Action;

    public static IResult Action(EmplooyeRequest categoryRequest, ApplicationDbContext context)
    {
        var category = new Category(categoryRequest.Name,"Rebeca", "Rebeca");


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


