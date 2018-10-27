using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CV_ASP.NET_LECT.Models;

namespace CV_ASP.NET_LECT.Controllers
{
    [Route("[controller]/[action]")]
    public class JobOfferController : Controller
    {
        private static List<JobOffer> _offers = new List<JobOffer> {
            new JobOffer{ID = 1, JobTitle="Junior Developer", JobDescription="The Junior Software Developer is part of an agile development team building and working on"+
                                                                               "enterprise grade software systems on top of the Microsoft .NET development stack. The Junior"+
                                                                                "Software Developer is involved in all areas of development from design to development to testing"},
            new JobOffer{ID = 2, JobTitle="Backend Developer" , JobDescription="If you have excellent programming skills and a passion for developing applications or improving"+ 
                                                                                "existing ones, we would like to meet you. As a Back-end developer, you’ll work closely with our "+ 
                                                                                "engineers to ensure system consistency and improve user experience."},
            new JobOffer{ID = 3, JobTitle="Database Administrator", JobDescription="Responsibility as a database administrator (DBA) will be the performance, "+
                                                                                   "integrity and security of a database. You'll be involved in the planning and development "+
                                                                                   "of the database, as well as in troubleshooting any issues on behalf of the users."},
            new JobOffer{ID = 4, JobTitle="Business Analyst", JobDescription="Business Analyst who will be the vital link between our information technology capacity and our"+
                                                                             " business objectives by supporting and ensuring the successful completion of analytical, building,"+
                                                                             " testing and deployment tasks of our software product’s features."},
            new JobOffer{ID = 5, JobTitle="Junior Scrum Master", JobDescription="Scrum master is known as guardian of Scrum Team, someone that resolves impediment and have control"+
                                                                                " over the scrum processes. Those are major responsibilities that come in to mind when thinking of a scrum master."+
                                                                                " Scrum master job description is everything from coaching team about agile scrum to delivering successful deliverables."},
        };

        public IActionResult Index()
        {
            return View(_offers);
        }

        public IActionResult Details(int ID)
        {  
            return View(_offers.FirstOrDefault(o => o.ID == ID));
        }
    }
}