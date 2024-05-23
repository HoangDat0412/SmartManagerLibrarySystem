using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartManagerLibrarySystem.Models
{
    public class BorrowViewModel
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public DateTime LoanDate { get; set; }

        [Required]
        [Display(Name = "Return Date")]
        [FutureDate(ErrorMessage = "Return date must be in the future.")]
        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }
    }
}

public class FutureDateAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        DateTime returnDate = (DateTime)value;
        return returnDate > DateTime.Now;
    }
}