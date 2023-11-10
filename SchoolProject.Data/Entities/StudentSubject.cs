using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
    public class StudentSubject
    {
        //[Key]
        //public int StudSubID { get; set; }
        [Key]
        public int StudID { get; set; }
        [Key]
        public int SubID { get; set; }

        [ForeignKey("StudID")]
        [InverseProperty("studentSubjects")]
        public virtual Student Student { get; set; }

        [ForeignKey("SubID")]
        [InverseProperty("StudentSubjects")]
        public virtual Subject Subject { get; set; }

    }
}
