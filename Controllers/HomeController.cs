using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using EMI_Calculator.Data;
using EMI_Calculator.Models;
using EMI_Calculator.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EMI_Calculator.Controllers
{
    public class HomeController : Controller
    {
       
       
        
       
        public static  List<TransactionDetail> detailsoftransaction;
        static HomeController()
        {
            detailsoftransaction = new List<TransactionDetail>();
        }
        public HomeController()
        {
            
           
            
        }
       
     



        public  IActionResult Index()
        {
            

            return View();
        }
        public IActionResult Calculate()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Calculate(LoanDetailsViewModel model)
        {
           
            if (ModelState.IsValid)
            {
                

                LoanData existData = DapperMethods.GetLoanIdNew(model.LoanData.LoanAmount, model.LoanData.RateOfInterest, model.LoanData.Installments);

                if (existData != null)
                {
                    ViewBag.Error = "Data is already Exist!!!!";
                    detailsoftransaction = new List<TransactionDetail>();


                    return View();

                }
                else
                {

                  var data=  DapperMethods.AddLoanData(model.LoanData);
                  
                    foreach(var item in detailsoftransaction)
                    {
                        DapperMethods.AddTransactionDetails(item, data);

                    }
                    detailsoftransaction = new List<TransactionDetail>();

                    return RedirectToAction("TransactionDetail");

                }

               


            }
            return View(model);


        }

        public IActionResult TransactionDetail()
        {
       
            return View();
        }






        public JsonResult AjaxMethod(double loanAmount, double rateOfInterest, double installment, double monthlyRateOfInterest, double emi)
        {

            

             var data = TransactionCalculation(1, loanAmount, rateOfInterest, installment, monthlyRateOfInterest, emi);


            LoanDetailsViewModel loanDetailsVM = new LoanDetailsViewModel()
            {


                LoanData = new LoanData()
                {
                    LoanAmount = loanAmount,
                    RateOfInterest = rateOfInterest,
                    Installments = installment,
                    MonthlyRateOfInterest = monthlyRateOfInterest,
                    EMI = emi
                },
                TransactionDetails = data


            };


            return Json(loanDetailsVM);

        }

        public List<TransactionDetail> TransactionCalculation(int loanId,double loanAmount, double rateOfInterest, double installment, double monthlyRateOfInterest, double emi)
        {
            var months = installment * 12;
            var monthlyInterest = monthlyRateOfInterest;


           // List<TransactionDetail> detailsoftransaction = new List<TransactionDetail>();

            var amount = loanAmount;


            for (int i = 0; i < months; i++)
            {
                TransactionDetail transaction = new TransactionDetail();
                if (i == 0)
                {
                    transaction.CumulativeInterest = 0;
                    transaction.OpeningAmonut = loanAmount;

                }
                else
                {
                    transaction.OpeningAmonut = detailsoftransaction.ElementAt(i - 1).ClosingAmount;
                }
                transaction.Installments = i + 1;


                transaction.LoanId = loanId;
                transaction.EMI = emi;
                transaction.Interest = (transaction.OpeningAmonut) * (monthlyInterest);
                transaction.Principal = (transaction.EMI) - (transaction.Interest);
                transaction.ClosingAmount = (transaction.OpeningAmonut) - (transaction.Principal);
                if (i == 0)
                {
                    transaction.CumulativeInterest = transaction.Interest;
                }
                else
                {
                    transaction.CumulativeInterest = (detailsoftransaction.ElementAt(i - 1).CumulativeInterest) + (transaction.Interest);

                }

               detailsoftransaction.Add(transaction);

            }
            return detailsoftransaction;
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
