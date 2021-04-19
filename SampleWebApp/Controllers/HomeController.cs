using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            string connectionString = @"Data Source=demosqlfelcu.database.windows.net;Initial Catalog=Demodb;User ID=federico;Password=Docker2020!!";
            ViewBag.Ciccio = string.Empty;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("SELECT TOP 2 * FROM TestTable", con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("{0} {1}", reader.GetInt32(0), reader.GetString(1));
                        ViewBag.Ciccio += string.Format("{0} {1}", reader.GetInt32(0), reader.GetString(1));
                    }
                }
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
