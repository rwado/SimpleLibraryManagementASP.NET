using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Library_Management.Models
{
    public class BorrowHistory
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        [Required]
        [Display(Name = "Borrowing Date")]
        public DateTime BorrowDate { get; set; }
        [Display(Name = "Returning Date")]
        public DateTime? ReturnDate { get; set; }
    }
}