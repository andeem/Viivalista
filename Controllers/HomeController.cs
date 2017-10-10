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
using System.Security.Claims;

namespace Viivalista.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {

            ViewData["tyontekijat"] = Tyontekija.all();
            return View(Vuoro.all());
        }



        public IActionResult Paivalista()
        {
            ViewData["tyontekijat"] = Tyontekija.getAllWithPermissions();
            return View();
        }


        [HttpPost]
        public IActionResult Tallenna(IEnumerable<Vuoro> vuorot)
        {
            return View("Index");
        }




        public IActionResult Tyopisteet()
        {

            return View();
        }

        public IActionResult Tyopiste()
        {

            return View();
        }





        public IActionResult Error()
        {
            return View();
        }
    }
}
