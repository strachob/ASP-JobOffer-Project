using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CV_ASP.NET_LECT.Models;
using System.Net;
using CV_ASP.NET_LECT.EntityFramework;

namespace CV_ASP.NET_LECT.Controllers
{
    [Route("[controller]/[action]")]
    public class JobOfferController : Controller
    {
        private readonly DataContext _context;
        private static List<JobOffer> _offers;
        private static List<Company> _companies;
        private static List<JobApplication> _applications;

        public JobOfferController (DataContext context){
            _context = context;
            _offers = _context.JobOffers.ToList();
            _companies = _context.Companies.ToList();
            _applications = _context.JobApplications.ToList();
            foreach (JobOffer offer in _offers)
            {
                offer.Company = _companies.Find(c => c.Id == offer.CompanyId);
            }
        }


        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Index([FromQuery(Name = "search")] string searchString)
        {
            
            if (String.IsNullOrEmpty(searchString))
            {
                return View(searchString);
            }

            return View();
            //List<JobOffer> searchResult = _offers.FindAll(o => o.JobTitle.Contains(searchString));
            //return View(searchResult);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Details(int ID)
        {
            var model = new JobOfferDetailsView
            {
                JobOffer = _offers.FirstOrDefault(o => o.ID == ID),
                JobApplications = _applications.FindAll(j => j.OfferId == ID)
            };
            Console.WriteLine(model.JobOffer);
            return View(model);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
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
        [ApiExplorerSettings(IgnoreApi = true)]
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
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null){
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            _offers.RemoveAll(j => j.ID == id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult> Create()
        {
            var model = new JobOfferCreateView
            {
                Companies = _companies
            };
            return View(model);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult CompaniesList([FromQuery(Name = "search")] string searchString)
        {
            if (String.IsNullOrEmpty(searchString))
            {
                return View(_companies);
            }
            List<Company> searchResult = _companies.FindAll(o => o.Name.Contains(searchString));
            return View(searchResult);
        }

        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult> DeleteCompany(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            _companies.RemoveAll(j => j.Id == id);
            return RedirectToAction("CompaniesList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult> Create(JobOfferCreateView view)
        {
            if (!ModelState.IsValid)
            {
                view.Companies = _companies;
                return View(view);
            }

            JobOffer jo = new JobOffer
            {
                CompanyId = view.CompanyId,
                Company = _companies.FirstOrDefault(c => c.Id == view.CompanyId),
                JobDescription = view.JobDescription,
                JobTitle = view.JobTitle,
                Location = view.Location,
                SalaryFrom = view.SalaryFrom,
                SalaryTo = view.SalaryTo,
                ValidUntil = view.ValidUntil,
                Created = DateTime.Now
            };
            _context.JobOffers.Add(jo);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
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
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult> Apply(JobApplicationApplyView view)
        {
            if (!ModelState.IsValid)
            {
                view.JobOffer = _offers.Find(j => j.ID == view.OfferId);
                return View(view);
            }
            JobApplication ja = new JobApplication
            {
                OfferId = view.OfferId,
                FirstName = view.FirstName,
                LastName = view.LastName,
                PhoneNumber = view.PhoneNumber,
                EmailAddress = view.EmailAddress,
                ApplicationDescription = view.ApplicationDescription
            };
            _context.JobApplications.Add(ja);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = view.OfferId });
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult ApplicationDetails(int ID)
        {
            return View(_applications.FirstOrDefault(j => j.Id == ID));
        }

    }
}