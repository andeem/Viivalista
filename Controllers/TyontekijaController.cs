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
            List<Tyontekija> tyontekijat = new List<Tyontekija>();
            tyontekijat.AddRange(Tyontekija.all());
            
            return View(tyontekijat);
        }

        [Route("Tyontekijat")]
        [HttpPost]
        public IActionResult Tallenna(String nimi, String tyontekijaryhma)
        {
            new Tyontekija(nimi, tyontekijaryhma).save();
            ViewData["tyontekijat"] = Tyontekija.all();
            return View("Index");
        }

        [Route("Tyontekijat/{id}")]
        public IActionResult Nayta(int id)
        {

            
            return View("Tyontekija", Tyontekija.find(id));
        }

        public IActionResult Lisaa()
        {
            return View();
        }

        [Route("Tyontekijat/Muokkaa/{id}")]
        public IActionResult Muokkaa(int id)
        {
            Tyontekija t = Tyontekija.find(id);
            ViewData["tyontekija"] = t;
            ViewData["title"] = t.nimi;
            return View();
        }

        [Route("Tyontekijat/{id}")]
        [HttpPost]
        public IActionResult Tallenna(int id, String nimi, String tyontekijaryhma)
        {
            Tyontekija t = Tyontekija.find(id);
            t.nimi = nimi;
            t.tyontekijaryhma = tyontekijaryhma;
            t.update();
            ViewData["tyontekija"] = t;
            ViewData["title"] = t.nimi;
            return View("Tyontekija");
        }

        
        public IActionResult Poista(int id)
        {
            Tyontekija.find(id).delete();
            ViewData["tyontekijat"] = Tyontekija.all();
            return RedirectToAction("Index");
        }

    }
}
