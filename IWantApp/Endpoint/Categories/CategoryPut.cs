using IWantApp.Domain.Products;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Mvc;

namespace IWantApp.Endpoint.Categories
{
    public static class CategoryPut
    {
       
        public static string Template => "/categories/{id:guid}";
        public static string[] Method => new string[] { HttpMethod.Put.ToString() };

        public static Delegate Handle => Action;

        public static IResult Action([FromRoute] Guid Id,EmplooyeRequest categoryRequest, ApplicationDbContext context)
        {
            var category = context.Categories.Where(c => c.Id == Id).FirstOrDefault();

            if (category == null)
                return Results.NotFound();

            category.EditInfo(categoryRequest.Name, categoryRequest.Active);

            if (!category.IsValid)
                return Results.ValidationProblem(category.Notifications.ConvertToProblemsDetails());

            context.SaveChanges();
            return Results.Ok();


        }

        
    }


}
