using SchoolProject.Data.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
    public class Subject : GeneralLocalizableEntity
    {
        public Subject()
        {
            StudentSubjects = new HashSet<StudentSubject>();
            DepartmentSubjects = new HashSet<DepartmetSubject>();
            InstructorSubjects = new HashSet<InstructorSubject>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubID { get; set; }

        [StringLength(200)]
        public string? SubjectNameAr { get; set; }

        [StringLength(200)]
        public string? SubjectNameEn { get; set; }
        public int? Period { get; set; }
        public decimal? Degree { get; set; }

        [InverseProperty("Subject")]
        public virtual ICollection<StudentSubject> StudentSubjects { get; set; }

        [InverseProperty("Subject")]
        public virtual ICollection<DepartmetSubject> DepartmentSubjects { get; set; }

        [InverseProperty("Subject")]
        public virtual ICollection<InstructorSubject> InstructorSubjects { get; set; }
    }
}
