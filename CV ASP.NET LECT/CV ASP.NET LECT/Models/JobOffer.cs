using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CV_ASP.NET_LECT.CustomValidation;

namespace CV_ASP.NET_LECT.Models
{
    public class JobOffer {
        public int ID { get; set; }
        [Display(Name = "Job title")]
        [Required]
        public string JobTitle { get; set; }
        public virtual Company Company { get; set; }
        public virtual int CompanyId { get; set; }
        [Display(Name = "Salary from")]
        [DataType(DataType.Currency)]
        [SalaryFromGreaterThenZero]
        public decimal? SalaryFrom { get; set; }
        [Display(Name = "Salary to")]
        [DataType(DataType.Currency)]
        [SalaryToGreaterThenZero]
        public decimal? SalaryTo { get; set; }
        public DateTime Created { get; set; }
        public string Location { get; set; }
        [Required]
        [MinLength(50)]
        public string JobDescription { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyy-MM-dd}")]
        [Display(Name = "Valid until")]
        [DateGreaterThanNow]
        public DateTime? ValidUntil { get; set; }
    }
}
