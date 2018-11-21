using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CV_ASP.NET_LECT.Models
{
    public class JobApplicationsViewModel
    {
        public IEnumerable<JobApplication> JobApplications { get; set; }
        public int TotalPage { get; set; }
    }
}
