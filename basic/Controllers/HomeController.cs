using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace basic.Controllers
{
    public class HomeController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public IActionResult Authenticate()
        {
            var grandmaClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Bob"),
                new Claim(ClaimTypes.Email, "Bob@Bob.com"),
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
    }
}