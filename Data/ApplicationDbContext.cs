using System;
using System.Collections.Generic;
using System.Text;
using EMI_Calculator.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EMI_Calculator.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<LoanData> LoanData { get; set; }
        public DbSet<TransactionDetail> TransactionDetails{ get; set; }
    }
}
