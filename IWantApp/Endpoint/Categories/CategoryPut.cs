using IWantApp.Domain.Products;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IWantApp.Endpoint.Categories
{
    public static class CategoryPut
    {
       
        public static string Template => "/categories/{id:guid}";
        public static string[] Method => new string[] { HttpMethod.Put.ToString() };

        public static Delegate Handle => Action;

        public static IResult Action([FromRoute] Guid Id, HttpContext http, ProductRequest categoryRequest, ApplicationDbContext context)
        {
            var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var category = context.Categories.Where(c => c.Id == Id).FirstOrDefault();

            if (category == null)
                return Results.NotFound();

            category.EditInfo(categoryRequest.Name, categoryRequest.Active, userId);

            if (!category.IsValid)
                return Results.ValidationProblem(category.Notifications.ConvertToProblemsDetails());

            context.SaveChanges();
            return Results.Ok();


        }

        
    }


}
