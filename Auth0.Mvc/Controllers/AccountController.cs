using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth0.Mvc.Controllers;

public class AccountController : Controller
{
    [AllowAnonymous]
    public async Task Login(string returnUrl = "/")
    {
        var authProps = new LoginAuthenticationPropertiesBuilder()
            .WithRedirectUri(returnUrl)
            .Build();
        await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authProps);
    }
    
    [Authorize]
    public async Task Logout()
    {
        var authProps = new LogoutAuthenticationPropertiesBuilder()
            .WithRedirectUri(Url.Action("Index", "Home")!)
            .Build();
        await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authProps);
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}