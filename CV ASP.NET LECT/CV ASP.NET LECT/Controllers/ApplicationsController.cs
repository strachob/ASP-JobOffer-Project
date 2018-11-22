using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CV_ASP.NET_LECT.EntityFramework;
using CV_ASP.NET_LECT.Models;

namespace CV_ASP.NET_LECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly DataContext _context;

        public ApplicationsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Applications
        /// <summary>
        /// Get Applications with specific offerID and from selected pageNumber
        /// </summary>
        /// <remarks>Standard pageNo is 1 and pageSize is 4</remarks>
        [HttpGet]
        public JobApplicationsViewModel GetApplications(int offerID, int pageNo = 1)
        {
            int totalPage, totalRecord, pageSize;
            pageSize = 4;

            totalRecord = _context.JobApplications.Count();
            totalPage = (totalRecord / pageSize) + ((totalRecord % pageSize) > 0 ? 1 : 0);
            var record = (from u in _context.JobApplications
                          where u.OfferId == offerID
                          orderby u.FirstName, u.LastName
                          select u).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

            JobApplicationsViewModel empData = new JobApplicationsViewModel
            {
                JobApplications = record,
                TotalPage = totalPage
            };

            return empData;
        }
    }
}