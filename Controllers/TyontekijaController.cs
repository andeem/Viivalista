﻿using System;
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
            ViewData["tyontekijat"] = tyontekijat;
            return View();
        }

        [Route("Tyontekijat")]
        [HttpPost]
        public IActionResult Tallenna(String nimi, String tyontekijaryhma)
        {
            Tyontekija.save(new Tyontekija(nimi, tyontekijaryhma));
            ViewData["tyontekijat"] = Tyontekija.all();
            return View("Index");
        }

        [Route("Tyontekijat/{id}")]
        public IActionResult Nayta(int id)
        {
            
            ViewData["tyontekija"] = Tyontekija.find(id);
            return View("Tyontekija");
        }
        public IActionResult MuokkaaTyontekija()
        {

            return View();
        }
    }
}
