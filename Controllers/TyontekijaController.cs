﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Viivalista.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Viivalista.Controllers
{
    [Authorize(Policy = "Esimies")]
    public class TyontekijaController : BaseController
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
        public IActionResult Tallenna(Tyontekija t)
        {
            if (ModelState.IsValid)
            {
                t.save();
            }
            
            
            return View("Index", Tyontekija.all());
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

            return View(Tyontekija.find(id));
        }

        [Route("Tyontekijat/{id}")]
        [HttpPost]
        public IActionResult TallennaVanha(Tyontekija t)
        {
            if (ModelState.IsValid)
            {
                t.update();
            }
            
            return View("Tyontekija", t);
        }

        
        public IActionResult Poista(Tyontekija t)
        {
            t.delete();
            
            return RedirectToAction("Index", Tyontekija.all());
        }

    }
}
