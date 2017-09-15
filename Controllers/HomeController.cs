using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Text;
using Npgsql;
using Viivalista.lib;

namespace Viivalista.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            NpgsqlConnection conn = Database.connection();
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                ViewData["Message"] = "Tietokantayhteys toimii";
            }
            conn.Close();
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


        public IActionResult Error()
        {
            return View();
        }
    }
}
