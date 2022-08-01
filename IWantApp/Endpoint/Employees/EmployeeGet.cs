using Dapper;
using IWantApp.Domain.Products;
using IWantApp.Endpoint.Categories;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace IWantApp.Endpoint.Employees;

public static class EmployeeGet
{
   
    public static string Template => "/employees";
    public static string[] Method => new string[] { HttpMethod.Get.ToString() };

    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]

    public static IResult Action(int? page, int? rows, QueryAllUsersWithClaimName query) 
    {
        if(!page.HasValue)
        {
            page = 0;
        }
        if(!rows.HasValue)
        {
            rows = 0;
        }

        try
        {
            return Results.Ok(query.SelectQuery(page.Value, rows.Value));
        }
        catch (Exception)
        {

            return Results.BadRequest("Erro: Insira valores válidos diferentes de zero...");
        }
        



    }


    

    
}


