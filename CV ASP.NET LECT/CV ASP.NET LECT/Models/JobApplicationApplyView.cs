using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CV_ASP.NET_LECT.Models
{
    public class JobApplicationApplyView : JobApplication
    {
        public JobOffer JobOffer { get; set; }
    }
}
