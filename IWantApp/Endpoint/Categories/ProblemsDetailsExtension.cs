using Flunt.Notifications;
using Microsoft.AspNetCore.Identity;
using System.Collections;

namespace IWantApp.Endpoint.Categories;

public static class ProblemsDetailsExtension
{

    //Extension Method 
    public static Dictionary<string, string[]> ConvertToProblemsDetails(this IReadOnlyCollection<Notification> notification) 
    {
        //Agroup the keys(name,createdby,editedby), turns them into ToDicionary at the same time it seeks the key to the function
        return notification
            .GroupBy(g => g.Key)
                .ToDictionary(g => g.Key, g => g.Select(x => x.Message).ToArray());
    }

    public static Dictionary<string, string[]> ConvertToProblemsDetails(this IEnumerable<IdentityError> error)
    {
        var dictionary = new Dictionary<string, string[]>();    
        dictionary.Add("Error", error.Select(e => e.Description).ToArray());
        return dictionary;
    }
}
