using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryMVC.Models
{
    public class BookViewModel
    {
        public int BookId { get; set; }

        public string Title { get; set; }

        [Display(Name = "Serial Number")]
        public string SerialNumber { get; set; }

        public string Author { get; set; }

        public string Publisher { get; set; }
        public string Genre { get; set; }
        [Display(Name = "Pages")]
        public int NumberOfPages { get; set; }
        [Display(Name = "Year")]
        public int YearOfPublishing { get; set; }
        public bool IsAvailable { get; set; }
        public string FilePath { get; set; }
    }
}