using IWantApp.Domain.Products;
using IWantApp.Endpoint.Categories;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IWantApp.Endpoint.Security;

public static class TokenPost
{
   
    public static string Template => "/token";
    public static string[] Method => new string[] { HttpMethod.Post.ToString() };

    public static Delegate Handle => Action;

    [AllowAnonymous]

    public static IResult Action(LoginRequest loginRequest, IConfiguration configuration, UserManager<IdentityUser> userManager)
    {
        var user = userManager.FindByEmailAsync(loginRequest.Email).Result;

        if (user == null)
            return Results.BadRequest();

        if (!userManager.CheckPasswordAsync(user, loginRequest.Password).Result)
            return Results.BadRequest();


        var claims = userManager.GetClaimsAsync(user).Result;
        var subject = new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Email, loginRequest.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
        });
        subject.AddClaims(claims);

        var key = Encoding.ASCII.GetBytes(configuration["JwtBearerTokenSettings:SecretKey"]);
        var tokenDescriptor = new SecurityTokenDescriptor  // generate token
        {
            Subject = subject,
            SigningCredentials =
            new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature), //passing signing credentials
            Audience = configuration["JwtBearerTokenSettings:Audience"], //who will use
            Issuer = configuration["JwtBearerTokenSettings:Issuer"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return Results.Ok(new
        {
            token = tokenHandler.WriteToken(token)
        });
            

        


    }


    

    
}


