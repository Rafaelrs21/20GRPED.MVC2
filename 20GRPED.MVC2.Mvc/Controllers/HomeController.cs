﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using _20GRPED.MVC2.Domain.Model.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using _20GRPED.MVC2.Mvc.Models;
using Microsoft.Extensions.Options;

namespace _20GRPED.MVC2.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOptionsMonitor<TestOption> _testOption;

        public HomeController(
            ILogger<HomeController> logger,
            IOptionsMonitor<TestOption> testOption)
        {
            _logger = logger;
            _testOption = testOption;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
