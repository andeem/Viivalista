﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections;
using System.Text;
using Npgsql;
using Viivalista.lib;
using Viivalista.Models;

namespace Viivalista.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {


            return View();
        }



        public IActionResult Paivalista()
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





        public IActionResult Error()
        {
            return View();
        }
    }
}
