using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
    public class InstructorSubject
    {
        [Key]
        public int SubID { get; set; }
        [Key]
        public int InstructorId { get; set; }

        [ForeignKey(nameof(InstructorId))]
        [InverseProperty("InstructorSubjects")]
        public Instructor Instructor { get; set; }

        [ForeignKey(nameof(SubID))]
        [InverseProperty("InstructorSubjects")]
        public Subject Subject { get; set; }

    }
}
