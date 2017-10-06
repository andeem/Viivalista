﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Viivalista.Models;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Viivalista.Controllers
{
    public class HuomioController : BaseController
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            if (GetUserLoggedIn() != null)
            {
                return View(Huomio.HaeTyontekijalla(GetUserLoggedIn()));
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
        }
        public IActionResult MuokkaaHuomio()
        {

            return View();
        }
        public IActionResult Lisaa()
        {

            return View();
        }
        public IActionResult Tallenna(Huomio h)
        {
            if (ModelState.IsValid)
            {
                h.save();
            }


            return View("Index", Huomio.HaeTyontekijalla(GetUserLoggedIn()));
        }
    }
}
