using System.Linq;
using System.Security.Claims;

namespace PaceMe.BlazorApp.Utilities
{
    public static class ClaimsPrincipalExtensions {
        public static string GetMsalUserId(this ClaimsPrincipal User){

            return User.Claims.First(x => x.Type == "oid").Value;
        }
    }
}