using Dapper;
using IWantApp.Domain.Products;
using IWantApp.Endpoint.Categories;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace IWantApp.Endpoint.Clients;

public static class ClientGet
{
   
    public static string Template => "/clients";
    public static string[] Method => new string[] { HttpMethod.Get.ToString() };

    public static Delegate Handle => Action;

    [AllowAnonymous]

    public static async Task<IResult> Action(HttpContext http)
    {
        var user = http.User;
        var result = new
        {
            Id = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value,
            Cpf = user.Claims.First(c => c.Type == "Cpf").Value,
            Name = user.Claims.First(c => c.Type == "Name").Value
            

        };

        return Results.Ok(result);
       
           
        
   
    }


    

    
}


