using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryMVC.Models
{
    public class Book
    {
        public int BookId { get; set; }
        [Required]
        public string Title { get; set; }

        [Required]
        [Display(Name="Serial Number")]
        public string SerialNumber { get; set; }

        public string Author { get; set; }
        public string Publisher { get; set; }
        public string Genre { get; set; }
        [Display(Name = "Pages")]
        public int NumberOfPages { get; set; }
        [Display(Name = "Year")]
        public int YearOfPublishing { get; set; }
        public ICollection<BorrowHistory> BorrowHistories { get; set; }
       //public string FilePath { get; set; }
    }
}