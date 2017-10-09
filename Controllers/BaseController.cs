using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Viivalista.Models;
using System.Security.Claims;

namespace Viivalista.Controllers
{
    public abstract partial class BaseController : Controller
    {


        public BaseController()
        {
            
        }

        public Kayttaja GetUserLoggedIn()
        {
            if (HttpContext.User != null)
            {
                return Kayttaja.get(int.Parse(HttpContext.User.FindFirst(ClaimTypes.PrimarySid).Value));
            } else
            {
                return null;
            }
        }

        public async Task<IActionResult> KirjauduUlos()
        {

            await HttpContext.Authentication.SignOutAsync("MyCookieAuthenticationScheme");

            return RedirectToAction("Index");
        }

        public IActionResult Kirjautuminen()
        {
            return View("Kirjautuminen");
        }

        [HttpPost]
        public async Task<IActionResult> Kirjautuminen(Kayttaja k)
        {
            Kayttaja kayt = Kayttaja.get(k.Kayttajatunnus, k.Salasana);
            if (kayt != null)
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.PrimarySid, kayt.Id.ToString()),
                                               new Claim(ClaimTypes.Name, kayt.Kayttajatunnus, ClaimValueTypes.String),
                                               
                                               new Claim(ClaimTypes.Role, kayt.Esimies.ToString(), ClaimValueTypes.String),
                                               };
                var userIdentity = new ClaimsIdentity(claims, "MyAuth");
                var userPrincipal = new ClaimsPrincipal(userIdentity);
                await HttpContext.Authentication.SignInAsync("MyCookieAuthenticationScheme", userPrincipal);

            }
            return RedirectToAction("Index");
        }




    }

}
