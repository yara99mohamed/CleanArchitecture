using SchoolProject.Data.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
    public partial class Department : GeneralLocalizableEntity
    {
        public Department()
        {
            Students = new HashSet<Student>();
            DepartmentSubjects = new HashSet<DepartmetSubject>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DID { get; set; }

        public int? InstructorManagerId { get; set; }

        public string? DNameAr { get; set; }

        public string? DNameEn { get; set; }

        [InverseProperty("Department")]
        public virtual ICollection<Student> Students { get; set; }

        [InverseProperty("Department")]
        public virtual ICollection<DepartmetSubject> DepartmentSubjects { get; set; }

        [InverseProperty("Department")]
        public virtual ICollection<Instructor> Instructors { get; set; }

        [ForeignKey(nameof(InstructorManagerId))]
        [InverseProperty("DepartmentManager")]
        public virtual Instructor? InstructorManager { get; set; }
    }
}
