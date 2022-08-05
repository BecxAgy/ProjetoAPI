using IWantApp.Endpoint.Categories;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace IWantApp.Endpoint.Products
{
    public static class ProductPost
    {
        public static string Template = "/products";
        public static string[] Method = new string[]{HttpMethods.Post.ToString()};
        public static Delegate Handle = Action;

        [Authorize(Policy = "EmployeePolicy")]

        public static async Task<IResult> Action(ApplicationDbContext context, HttpContext http, ProductRequest productRequest)
        {
            var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value; //This line says who is saving a product
            var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == productRequest.CategoryId);
            var product = new Product(productRequest.Name, productRequest.Description, category, productRequest.HasStock, userId, productRequest.Price);

            if (!product.IsValid)
            {
                return Results.ValidationProblem(product.Notifications.ConvertToProblemsDetails());
            }

            await context.Products.AddAsync(product);   
            await context.SaveChangesAsync();

            return Results.Created($"/products/{product.Id}", product.Id);
        }
    }
}
