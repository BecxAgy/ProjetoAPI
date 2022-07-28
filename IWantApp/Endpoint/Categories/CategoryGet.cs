using IWantApp.Domain.Products;
using IWantApp.Infra.Data;

namespace IWantApp.Endpoint.Categories
{
    public static class CategoryGet
    {
       
        public static string Template => "/categories";
        public static string[] Method => new string[] { HttpMethod.Get.ToString() };

        public static Delegate Handle => Action;

        public static IResult Action(ApplicationDbContext context)
        {
            var categories = context.Categories.ToList();
            var response = categories.Select(c => new CategoryResponse { Id = c.Id, Name = c.Name, Active = c.Active });

            context.SaveChanges();

            return Results.Ok(response);

        }

        
    }


}
