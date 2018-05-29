using SelectFromDb.Data;
using SelectFromDb.DbModels;

namespace SelectFromDb.Data
{
	public partial class CourseSelect : Select<Course> {}
	public partial class StudentSelect : Select<Student> {}
	public partial class StudentCourseSelect : Select<StudentCourse> {}
}
