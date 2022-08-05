using IWantApp.Domain.Products;
using IWantApp.Domain.Users;
using IWantApp.Endpoint.Categories;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IWantApp.Endpoint.Employees;

public static class EmployeePost
{
   
    public static string Template => "/employees";
    public static string[] Method => new string[] { HttpMethod.Post.ToString() };

    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]

    public static async Task<IResult> Action(EmployeeRequest emplooyeRequest,HttpContext http,UserCreate userCreate)
    {
        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var userClaims = new List<Claim>()
        {
            new Claim("EmployeeCode", emplooyeRequest.employeeCode),
            new Claim("Name", emplooyeRequest.Name),
            new Claim("CreatedBy", userId)

        };
      
       (IdentityResult identity, string user) result = await userCreate.Create(emplooyeRequest.Email, emplooyeRequest.Name, userClaims);

        if (!result.identity.Succeeded)
            return Results.ValidationProblem(result.identity.Errors.ConvertToProblemsDetails());

        

        return Results.Created($"/employees/{result.user}", result.user);
    }


    

    
}


