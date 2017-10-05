using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections;
using System.Text;
using Npgsql;
using Viivalista.lib;
using Viivalista.Models;

namespace Viivalista.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {

            try
            {
                Kayttaja k = Kayttaja.get("matmei", "matmei");
                if (k != null)
                {
                    ViewData["Message"] = GetUserLoggedIn().Kayttajatunnus;
                }
            }
            catch
            {

            }
            return View();
        }

        public IActionResult Tyontekijat()
        {

            return View();
        }

        public IActionResult OmaSivu()
        {

            return View();
        }

        public IActionResult Paivalista()
        {

            return View();
        }

        public IActionResult MuokkaatyonTekija()
        {

            return View();
        }
        public IActionResult MuokkaaHuomio()
        {

            return View();
        }

        public IActionResult Tyopisteet()
        {

            return View();
        }

        public IActionResult Tyopiste()
        {

            return View();
        }
        public IActionResult Huomio()
        {

            return View();
        }
        public IActionResult Kirjautuminen()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Kirjautuminen(Kayttaja k)
        {
            Kayttaja kayt = Kayttaja.get(k.Kayttajatunnus, k.Salasana);
            if (kayt != null)
            {
                HttpContext.Session.SetInt32("userid", kayt.Id);
            }
            return RedirectToAction("Index");
        }


        public IActionResult Error()
        {
            return View();
        }
    }
}
