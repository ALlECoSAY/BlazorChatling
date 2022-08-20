using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorChatling.Data
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        public override Task<AuthenticationState> GetAuthenticationStateAsync() {

            /*var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "admin")
            }, "apiauth_type");
*/
            var identity = new ClaimsIdentity();

            var user = new ClaimsPrincipal(identity);


            return Task.FromResult(new AuthenticationState(user));


        }

        public void MarkUserAsAuthenticated(string name, string tag) {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, name),
                new Claim("Tag", tag)
            }, "apiauth_type");

            var user = new ClaimsPrincipal(identity);


            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));

        }

        public void MarkUserAsNotAuthenticated() {
            var identity = new ClaimsIdentity();

            var user = new ClaimsPrincipal(identity);


            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));


        }




    }
}
