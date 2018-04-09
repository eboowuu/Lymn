using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LymcWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LymcWeb.Controllers
{
    public class ReservationController : Controller
    {
        public IActionResult Index()
        {
            return Redirect("https://wunesvaderaniangularclient.azurewebsites.net");
        }
    }
}