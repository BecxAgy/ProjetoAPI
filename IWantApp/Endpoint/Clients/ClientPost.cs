using IWantApp.Domain.Users;
using IWantApp.Endpoint.Categories;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace IWantApp.Endpoint.Clients;

public static class ClientPost
{
   
    public static string Template => "/clients";
    public static string[] Method => new string[] { HttpMethod.Post.ToString() };

    public static Delegate Handle => Action;

    [AllowAnonymous]

    public static async Task<IResult> Action(ClientRequest clientRequest, UserCreate userCreate)
    {
     
        var userClaims = new List<Claim>()
        {
            new Claim("CPF", clientRequest.Cpf),
            new Claim("Name",clientRequest.Name),

        };

        (IdentityResult identity, string userId) result = await userCreate.Create(clientRequest.Email, clientRequest.Password, userClaims);

       if(!result.identity.Succeeded)
            return Results.ValidationProblem(result.identity.Errors.ConvertToProblemsDetails());


        return Results.Created($"/clients/{result.userId}", result.userId);
    }


    

    
}


