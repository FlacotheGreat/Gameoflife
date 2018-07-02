using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameOfLifeClean.Models;
using Newtonsoft.Json;

namespace GameOfLifeClean.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        //public string GetGridValues(string blockInfo)
        //{
        //    Block B = new Block();

        //    string info = JsonConvert.DeserializeObject<Block>(blockInfo);

        //    return B;

        //}

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
