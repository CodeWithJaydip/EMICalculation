using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMI_Calculator.Models.ViewModel
{
    public class LoanDetailsViewModel
    {
        public LoanData LoanData { get; set; }
        public List<TransactionDetail> TransactionDetails { get; set; }
    }
}
