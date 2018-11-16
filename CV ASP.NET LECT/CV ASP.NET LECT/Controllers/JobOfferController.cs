using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CV_ASP.NET_LECT.Models;
using System.Net;

namespace CV_ASP.NET_LECT.Controllers
{
    [Route("[controller]/[action]")]
    public class JobOfferController : Controller
    {
        
        private static List<JobOffer> _offers = new List<JobOffer> {
            new JobOffer{
                ID = 1,
                JobTitle ="Junior Developer",
                JobDescription ="The Junior Software Developer is part of an agile development team building and working on"+
                                "enterprise grade software systems on top of the Microsoft .NET development stack. The Junior"+
                                "Software Developer is involved in all areas of development from design to development to testing",
                Company = CompanyController._companies.FirstOrDefault(c => c.Name == "Microsoft"),
                Created = DateTime.Now.AddDays(-14),
                Location = "Dublin, Irlandia",
                SalaryFrom = 5000,
                SalaryTo = 8000,
                ValidUntil = DateTime.Now.AddDays(14)
            },
            new JobOffer{
                ID = 2,
                JobTitle ="Database Administrator",
                JobDescription ="Responsibility as a database administrator (DBA) will be the performance, "+
                                "integrity and security of a database. You'll be involved in the planning and development "+
                                "of the database, as well as in troubleshooting any issues on behalf of the users.",
                Company = CompanyController._companies.FirstOrDefault(c => c.Name == "KMD"),
                Created = DateTime.Now.AddDays(-2),
                Location = "Warszawa, Polska",
                SalaryFrom = 7500,
                SalaryTo = 10000,
                ValidUntil = DateTime.Now.AddDays(20)
            }
        };
        private static List<JobApplication> _applications = new List<JobApplication> {
            new JobApplication
            {
                Id = 1,
                OfferId = 2,
                FirstName = "Bartek",
                LastName = "Strachowski",
                PhoneNumber = "124294120",
                EmailAddress = "b.stra@gmail.com",
                ApplicationDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit,"+
                                         " sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam,"+
                                         " quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure"+"" +
                                         " dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur."+
                                         " Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
            }
        };

        [HttpGet]
        public IActionResult Index([FromQuery(Name = "search")] string searchString)
        {
            if (String.IsNullOrEmpty(searchString))
            {
                return View(_offers);
            }
            List<JobOffer> searchResult = _offers.FindAll(o => o.JobTitle.Contains(searchString));
            return View(searchResult);
        }

        public IActionResult Details(int ID)
        {
            var model = new JobOfferDetailsView
            {
                JobOffer = _offers.FirstOrDefault(o => o.ID == ID),
                JobApplications = _applications.FindAll(j => j.OfferId == ID)
            };
            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            if(id == null){
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            var offer = _offers.Find(j => j.ID == id);
            if(offer == null){
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
            return View(offer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(JobOffer offer)
        {
            if (!ModelState.IsValid){
                return View();
            }
            var newOffer = _offers.Find(j => j.ID == offer.ID);
            newOffer.JobTitle = offer.JobTitle;
            return RedirectToAction("Details", new { id = offer.ID });
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null){
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            _offers.RemoveAll(j => j.ID == id);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Create()
        {
            var model = new JobOfferCreateView
            {
                Companies = CompanyController._companies
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(JobOfferCreateView view)
        {
            if (!ModelState.IsValid)
            {
                view.Companies = CompanyController._companies;
                return View(view);
            }
            var id = CompanyController._companies.Max(j => j.Id) + 1;
            _offers.Add(new JobOffer
                {
                    ID = id,
                    CompanyId = view.CompanyId,
                    Company = CompanyController._companies.FirstOrDefault(c => c.Id == view.CompanyId),
                    JobDescription = view.JobDescription,
                    JobTitle = view.JobTitle,
                    Location = view.Location,
                    SalaryFrom = view.SalaryFrom,
                    SalaryTo = view.SalaryTo,
                    ValidUntil = view.ValidUntil,
                    Created = DateTime.Now
                }
            );
            return RedirectToAction("Index");

        }
        
        public ActionResult Apply(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            var offer = _offers.Find(j => j.ID == id);
            if (offer == null)
            {
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }

            var model = new JobApplicationApplyView
            {
                JobOffer = offer,
                OfferId = offer.ID
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Apply(JobApplicationApplyView view)
        {
            if (!ModelState.IsValid)
            {
                view.JobOffer = _offers.Find(j => j.ID == view.OfferId);
                return View(view);
            }
            var id = _applications.Max(j => j.Id) + 1;
            _applications.Add(new JobApplication
            {
                Id = id,
                OfferId = view.OfferId,
                FirstName = view.FirstName,
                LastName = view.LastName,
                PhoneNumber = view.PhoneNumber,
                EmailAddress = view.EmailAddress,
                ApplicationDescription = view.ApplicationDescription
            });
            return RedirectToAction("Edit", new { id = view.OfferId });
        }

        public IActionResult ApplicationDetails(int ID)
        {
            return View(_applications.FirstOrDefault(j => j.Id == ID));
        }

    }
}