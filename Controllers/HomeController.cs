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

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
