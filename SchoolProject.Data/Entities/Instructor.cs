using SchoolProject.Data.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
    public class Instructor : GeneralLocalizableEntity
    {
        public Instructor()
        {
            InstructorSupervisors = new HashSet<Instructor>();
            InstructorSubjects = new HashSet<InstructorSubject>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InstructorId { get; set; }
        public int? DId { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? Address { get; set; }
        public string? Position { get; set; }
        public int? SupervisorId { get; set; }
        public decimal? Salary { get; set; }

        [ForeignKey(nameof(DId))]
        [InverseProperty("Instructors")]
        public Department? Department { get; set; }

        [InverseProperty("InstructorManager")]
        public Department? DepartmentManager { get; set; }

        [ForeignKey(nameof(SupervisorId))]
        [InverseProperty("InstructorSupervisors")]
        public Instructor? Supervisor { get; set; }

        [InverseProperty("Supervisor")]
        public virtual ICollection<Instructor> InstructorSupervisors { get; set; }

        [InverseProperty("Instructor")]
        public virtual ICollection<InstructorSubject> InstructorSubjects { get; set; }

    }
}
