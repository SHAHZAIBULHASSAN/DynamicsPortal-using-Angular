namespace DynamicsPortal.Controllers
{
    using DynamicsPortal.Business;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Security.Claims;
    using System.Threading.Tasks;

    [Route("[controller]")]
    public class ConnectController : Controller
    {
        readonly IContactManager contactManager;
        public ConnectController(IContactManager contactManager) => this.contactManager = contactManager;

        [Route("login"), AllowAnonymous]
        public async Task<IActionResult> LoginAsync(string emailAddress, string password)
        {
            if(Request.Method == HttpMethod.Get.Method)
            {
                return View();
            }

            var contact = await this.contactManager.GetContactAsync(emailAddress, password);
            if(contact == null)
            {
                ViewBag.EmailAddress = emailAddress;
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, contact.Id.ToString()),
                new Claim(ClaimTypes.Email, contact.EmailAddress),
                new Claim(ClaimTypes.NameIdentifier, contact.Name)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return LocalRedirect("/");
        }

        [Route("logout")]
        public async Task LogoutAsync() => await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme, new AuthenticationProperties { RedirectUri = "/" });
    }
}
