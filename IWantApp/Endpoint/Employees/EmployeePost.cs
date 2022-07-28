using IWantApp.Domain.Products;
using IWantApp.Endpoint.Categories;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IWantApp.Endpoint.Employees;

public static class EmployeePost
{
   
    public static string Template => "/employees";
    public static string[] Method => new string[] { HttpMethod.Post.ToString() };

    public static Delegate Handle => Action;

    public static IResult Action(EmployeeRequest emplooyeRequest, UserManager<IdentityUser> userManager)
    {
        //always will be used IdentityUser class to represent the users
        //Is not allowed pass the password in IdentityUser, it is pass in userMenager
        var user = new IdentityUser {UserName = emplooyeRequest.Email, Email = emplooyeRequest.Email};  
        //CreateAssync returns a result
        var result = userManager.CreateAsync(user, emplooyeRequest.Password).Result;
        if (!result.Succeeded)
            return Results.ValidationProblem(result.Errors.ConvertToProblemsDetails());

        //Claim´s List that help to refactor the code
        var userClaims = new List<Claim>()
        {
            new Claim("EmployeeCode", emplooyeRequest.employeeCode),
            new Claim("Name", emplooyeRequest.Name)

        };

        var resultClaim = userManager.AddClaimsAsync(user, userClaims).Result;
        if (!resultClaim.Succeeded)
            return Results.BadRequest(resultClaim.Errors.First());



        return Results.Created($"/employees/{user.Id}", user.Id);
    }


    

    
}


