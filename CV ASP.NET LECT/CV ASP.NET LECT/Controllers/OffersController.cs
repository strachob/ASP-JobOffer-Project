using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CV_ASP.NET_LECT.Models;
using CV_ASP.NET_LECT.EntityFramework;

namespace CV_ASP_NET_LECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OffersController : ControllerBase
    {

        private readonly DataContext _context;

        public OffersController(DataContext context)
        {
            _context = context;
        }
        // GET: api/Offers
        /// <summary>
        /// Get all offers for now
        /// </summary>
        /// <remarks>Standard pageNo is 1 and pageSize is 10</remarks>
        [HttpGet]
        public JobOfferViewModel GetOffers(string searchString, int pageNo = 1)
        {
            Console.WriteLine(searchString);
            int totalPage, totalRecord, pageSize;
            pageSize = 10;

            totalRecord = _context.JobOffers.Count();
            totalPage = (totalRecord / pageSize) + ((totalRecord % pageSize) > 0 ? 1 : 0);
            var record = (from u in _context.JobOffers
                          orderby u.JobTitle, u.Created
                          select u).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

            List<Company> _companies = _context.Companies.ToList();
            foreach (JobOffer offer in record)
            {
                offer.Company = _companies.Find(c => c.Id == offer.CompanyId);
            }
            JobOfferViewModel empData = new JobOfferViewModel
            {
                JobOffers = record,
                TotalPage = totalPage
            };

            return empData;
        }
    }
}