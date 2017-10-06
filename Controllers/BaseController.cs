using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Viivalista.Models;


namespace Viivalista.Controllers
{
    public abstract partial class BaseController : Controller
    {


        public BaseController()
        {
            
        }

        public Kayttaja GetUserLoggedIn()
        {
            if (HttpContext.Session.GetInt32("userid") != null)
            {
                return Kayttaja.get((int)HttpContext.Session.GetInt32("userid"));
            } else
            {
                return null;
            }
        }

        public IActionResult KirjauduUlos()
        {

            if (HttpContext.Session.GetInt32("userid") != null)
            {
                HttpContext.Session.Clear();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Kirjautuminen()
        {
            return View("Kirjautuminen");
        }
        [HttpPost]
        public IActionResult Kirjautuminen(Kayttaja k)
        {
            Kayttaja kayt = Kayttaja.get(k.Kayttajatunnus, k.Salasana);
            if (kayt != null)
            {
                HttpContext.Session.SetInt32("userid", kayt.Id);
                HttpContext.Session.SetString("userRole", kayt.Esimies.ToString());
            }
            return RedirectToAction("Index");
        }




    }

}
