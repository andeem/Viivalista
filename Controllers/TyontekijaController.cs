using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Viivalista.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Viivalista.Controllers
{
    public class TyontekijaController : Controller
    {
        // GET: /<controller>/
        [Route("Tyontekijat")]
        public IActionResult Index()
        {
            IList<Tyontekija> tyontekijat = new List<Tyontekija>();
            tyontekijat.Add(new Tyontekija(1, "Muumipeikko", "Tuotanto"));
            tyontekijat.Add(new Tyontekija(2, "Nipsu", "Tuotanto"));
            tyontekijat.Add(new Tyontekija(3, "Pikku Myy", "Tuotanto"));

            ViewData["tyontekijat"] = tyontekijat;
            return View();
        }

        [Route("Tyontekijat")]
        [HttpPost]
        public IActionResult Tallenna(Tyontekija t)
        {
            

            IList<Tyontekija> tyontekijat = new List<Tyontekija>();
            tyontekijat.Add(new Tyontekija(1, "Muumipeikko", "Tuotanto"));
            tyontekijat.Add(new Tyontekija(2, "Nipsu", "Tuotanto"));
            tyontekijat.Add(new Tyontekija(3, "Pikku Myy", "Tuotanto"));

            ViewData["tyontekijat"] = tyontekijat;
            return View("Index");
        }

        [Route("Tyontekijat/{id}")]
        public IActionResult Tyontekija(int id)
        {
            ViewData["tyontekija"] = new Tyontekija(1, "Muumipeikko", "Tuotanto");
            return View();
        }
        public IActionResult MuokkaaTyontekija()
        {

            return View();
        }
    }
}
