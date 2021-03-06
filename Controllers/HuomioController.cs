﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Viivalista.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Viivalista.Controllers
{   
    [Authorize(Policy = "Kirjautunut")]
    public class HuomioController : BaseController
    {
        // GET: /<controller>/
        [Route("Huomio")]
        public IActionResult Index()
        {
            return View(Huomio.HaeTyontekijalla(GetUserLoggedIn()));
        }
        public IActionResult MuokkaaHuomio()
        {

            return View();
        }

        //[Route("Huomio/{id}")]
        public IActionResult Nayta(int id)
        {


            return View("Huomio", Huomio.Hae(id));
        }
        [Route("Huomio/Lisaa")]
        public IActionResult Lisaa()
        {

            return View();
        }
        public IActionResult Muokkaa(int id)
        {

            return View(Huomio.Hae(id));
        }

        public IActionResult Tallenna(Huomio h)
        {
            if (ModelState.IsValid)
            {
                h.TyontekijaId = GetUserLoggedIn().Id;
                h.save();
            }


            return View("Huomio", Huomio.HaeTyontekijalla(GetUserLoggedIn()));
        }
        [HttpPost]
        [Route("Huomio/{id}")]
        public IActionResult TallennaVanha(Huomio h)
        {
            if (ModelState.IsValid)
            {
                
                h.edit();
            }


            return RedirectToAction("Nayta", h.Id);
        }
        
        public IActionResult Poista(Huomio h)
        {
            h.delete();

            return RedirectToAction("Index");
        }
    }
}
