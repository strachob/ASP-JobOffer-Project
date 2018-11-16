using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CV_ASP.NET_LECT.Models
{
    public class JobApplication
    {
        public int Id { get; set; }
        public int OfferId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        [MinLength(100)]
        public string ApplicationDescription { get; set; }
        public bool ContactAgreement { get; set; }
        public string CvUrl { get; set; }
    }
}
