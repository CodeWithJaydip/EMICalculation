using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EMI_Calculator.Models
{
    public class LoanData
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name ="Loan Amount")]
        public double LoanAmount { get; set; }

        [Required]
        [Display(Name = "Rate Of Interest")]
        public double RateOfInterest { get; set; }
        [Required]
        [Display(Name = "Installments")]
        public double Installments { get; set; }

        [Required]
        [Display(Name = "Monthly Rate Of Interest")]
        public double MonthlyRateOfInterest { get; set; }

        [Required]
        [Display(Name = "EMI")]
        public double EMI { get; set; }



    }
}
