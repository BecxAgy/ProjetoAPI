using IWantApp.Domain.Products;
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

    public static async Task<IResult> Action(EmployeeRequest emplooyeRequest,HttpContext http, UserManager<IdentityUser> userManager)
    {
        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        //always will be used IdentityUser class to represent the users
        //Is not allowed pass the password in IdentityUser, it is pass in userMenager
        var newUser = new IdentityUser {UserName = emplooyeRequest.Email, Email = emplooyeRequest.Email};
        //CreateAsync returns a result
        var result = await userManager.CreateAsync(newUser, emplooyeRequest.Password);

        if (!result.Succeeded)
            return Results.ValidationProblem(result.Errors.ConvertToProblemsDetails());

        //Claim´s List that help to refactor the code
        var userClaims = new List<Claim>()
        {
            new Claim("EmployeeCode", emplooyeRequest.employeeCode),
            new Claim("Name", emplooyeRequest.Name),
            new Claim("CreatedBy", userId)

        };

        var resultClaim = await userManager.AddClaimsAsync(newUser, userClaims);

        if (!resultClaim.Succeeded)
            return Results.BadRequest(resultClaim.Errors.First());



        return Results.Created($"/employees/{newUser.Id}", newUser.Id);
    }


    

    
}


