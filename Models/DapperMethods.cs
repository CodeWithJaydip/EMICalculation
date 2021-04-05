using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace EMI_Calculator.Models
{
    public class DapperMethods
    {
        private  IConfiguration _config;
        public DapperMethods(IConfiguration config)
        {
            _config = config;
        }


        public static string ConnectionString = "Server=.;Database=EMI_Calculator;Trusted_Connection=True;MultipleActiveResultSets=true";
        public static  int AddLoanData(LoanData data)
        {
            int loanId = 0;
           
            using (IDbConnection con = new SqlConnection(ConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@LoanAmount", data.LoanAmount);
                parameters.Add("@RateOfInterest", data.RateOfInterest);
                parameters.Add("@Installments",data.Installments);
                parameters.Add("@EMI",data.EMI);
                parameters.Add("@MonthlyRateOfInterest", data.MonthlyRateOfInterest);

                con.Execute("InsertLoanData", parameters, commandType: CommandType.StoredProcedure);
                 loanId = GetLoanIdNew(data.LoanAmount, data.RateOfInterest, data.Installments);

            }
            return loanId;
            
        }

        public static  LoanData GetLoanDataById(int id)
        {
            LoanData loanData = new LoanData();
            using (IDbConnection con = new SqlConnection(ConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@Id", id);
                loanData = con.Query<LoanData>("GetloanId", parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            return loanData;
        }

        public static int GetLoanIdNew(double loanAmount, double interest, double installments)
        {
            LoanData loanData = new LoanData();
            using (IDbConnection con = new SqlConnection(ConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@LoanAmount",loanAmount);
                parameters.Add("@RateOfInterest",interest);
                parameters.Add("@Installments",installments);

                loanData = con.Query<LoanData>("GetLoanIdNew",parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            return loanData.Id;
        }

        public static  int AddTransactionDetails(TransactionDetail data,int LoanId)
        {
            int rowAffected = 0;

            using (IDbConnection con = new SqlConnection(ConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Installments", data.Installments);
                parameters.Add("@OpenningAmount", data.OpeningAmonut);
                parameters.Add("@Principal", data.Principal);
                parameters.Add("@Interest ", data.Interest);
                parameters.Add("@EMI", data.EMI);
                parameters.Add("@ClosingAmount", data.ClosingAmount);
                parameters.Add(@"@CumulativeInterest", data.CumulativeInterest);
                parameters.Add("@LoanId", LoanId);

                rowAffected = con.Execute("InsertTransactionDetails", parameters, commandType: CommandType.StoredProcedure);

            }
            return rowAffected;
        }
    }
}
