using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Text;
using Npgsql;

namespace Viivalista.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            Uri url;
            bool isUrl = Uri.TryCreate(Environment.GetEnvironmentVariable("DATABASE_URL"), UriKind.Absolute, out url);
            if (isUrl)
            {
                var connectionUrl = $"host={url.Host};username={url.UserInfo.Split(':')[0]};password={url.UserInfo.Split(':')[1]};database={url.LocalPath.Substring(1)};pooling=true;";
                using (var conn = new NpgsqlConnection(connectionUrl))
                {
                    conn.Open();
                    if (conn.State is System.Data.ConnectionState.Open)
                    {
                        ViewData["Message"] = "Toimii";
                    }
                }
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
        public IActionResult Login()
        {

            return View();
        }


        public IActionResult Error()
        {
            return View();
        }
    }
}
