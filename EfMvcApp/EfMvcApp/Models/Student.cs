using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EfMvcApp.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        
        [StringLength(50)]
        public string LastName { get; set; }

        [Column("FirstName")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string FirstMidName { get; set; }
       
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate { get; set; }

        public string a { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}