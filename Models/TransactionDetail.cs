using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EMI_Calculator.Models
{
    public class TransactionDetail
    {
        [Key]
        public int Id { get; set; }
        public double Installments { get; set; }
        [Display(Name ="Opening")]
        public double OpeningAmonut { get; set; }
        public double Principal { get; set; }
        public double Interest { get; set; }
        public double EMI { get; set; }
        [Display(Name ="Closing")]
        public double ClosingAmount { get; set; }
        [Display(Name ="Cumulative Interest(CI)")]
        public double CumulativeInterest { get; set; }

        public int LoanId { get; set; }

        [ForeignKey("LoanId")]
        public LoanData LoanData { get; set; }
    }
}
