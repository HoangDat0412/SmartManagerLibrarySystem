using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartManagerLibrarySystem.Models
{
    public class LoanDetailViewModel
    {
        public Loans Loan { get; set; }
        public ApplicationUser User { get; set; }
        public Books Book { get; set; }
    }
}