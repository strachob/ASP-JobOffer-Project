using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CV_ASP.NET_LECT.Models;

namespace CV_ASP.NET_LECT.Controllers
{
    public class CompanyController : Controller
    {

        public static List<Company> _companies = new List<Company> {
            new Company(){Id = 1, Name = "Citi" },
            new Company(){Id = 2, Name = "KMD" },
            new Company(){Id = 3, Name = "Millenium" },
            new Company(){Id = 4, Name = "Microsoft" },
        };

        [HttpGet]
        public IActionResult Index([FromQuery(Name = "search")] string searchString)
        {
            if (String.IsNullOrEmpty(searchString))
            {
                return View(_companies);
            }
            List<Company> searchResult = _companies.FindAll(o => o.Name.Contains(searchString));
            return View(searchResult);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            _companies.RemoveAll(j => j.Id == id);
            return RedirectToAction("Index");
        }

        //public IActionResult Details(int Id)
        //{
        //    return View(_companies.FirstOrDefault(o => o.Id == Id));
        //}
    }
}