using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace basic.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthorizationService _authorizationService;
        public HomeController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }
        
        [Authorize(Policy = "Claim.DoB")]
        public IActionResult SecretPolicy()
        {
            return View();
        }

        public IActionResult Authenticate()
        {
            var grandmaClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Bob"),
                new Claim(ClaimTypes.Email, "Bob@Bob.com"),
                new Claim(ClaimTypes.DateOfBirth, "01/01/2000"),
                new Claim("MyOwnClaimType", "BobCustomClaim"),
            };
            
            var licenseClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Mr. Driver Drive"),
                new Claim(ClaimTypes.Email, "driver@driver.com"),
                new Claim("MyOwnClaimType", "DriveCustom"),
            };

            var grandmaIdentity = new ClaimsIdentity(grandmaClaims, "Gma IDentity");
            var licenseIdentity = new ClaimsIdentity(licenseClaims, "Gov");
            
            var userPrincipal = new ClaimsPrincipal(new[] {grandmaIdentity, licenseIdentity});

            HttpContext.SignInAsync(userPrincipal);
            
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DoStuff()
        {
            var builder = new AuthorizationPolicyBuilder("Schema");
            var customPolicy = builder.RequireClaim("Hello").Build();
            var result = await _authorizationService.AuthorizeAsync(User, "Claim.DoB");

            if (result.Succeeded)
            {
                return View("Index");
            }

            return View();
        }
    }
}