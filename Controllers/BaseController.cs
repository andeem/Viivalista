using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Viivalista.Models;

namespace Viivalista.Controllers
{
    public abstract partial class BaseController : Controller
    {


        public BaseController()
        {
            
        }

        public Kayttaja GetUserLoggedIn()
        {
            if (HttpContext.Session.GetInt32("userid") != null)
            {
                return Kayttaja.get((int)HttpContext.Session.GetInt32("userid"));
            } else
            {
                return null;
            }
        }


    }
}
