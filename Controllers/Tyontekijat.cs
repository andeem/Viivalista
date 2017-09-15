using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Viivalista.Controllers
{
    public class Tyontekijat : Controller
    {
        // GET: /<controller>/
        public IActionResult Lista()
        {
            return View();
        }
        public IActionResult Tyontekija()
        {

            return View();
        }
        public IActionResult MuokkaaTyontekija()
        {

            return View();
        }
    }
}
