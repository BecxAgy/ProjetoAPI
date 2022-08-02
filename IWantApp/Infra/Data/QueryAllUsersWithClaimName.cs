using Dapper;
using IWantApp.Endpoint.Employees;
using Microsoft.Data.SqlClient;
using System.Collections;

namespace IWantApp.Infra.Data
{
    public class QueryAllUsersWithClaimName
    {
        private IConfiguration configuration;

        public QueryAllUsersWithClaimName(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<IEnumerable<EmployeeResponse>> SelectQuery(int page, int rows) 
        {
            var db = new SqlConnection(configuration["ConnectionStrings:iWantDb"]);
            var querySelect = @"select Email, ClaimValue as Name 
            from AspNetUsers u inner join AspNetUserClaims
            c on u.Id = c.UserId and ClaimType = 'Name'
            order by name
            OFFSET(@page - 1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";

            var employee = await db.QueryAsync<EmployeeResponse>(
            querySelect,
            new { page, rows }
            );
            return employee;
        }
    }
}
