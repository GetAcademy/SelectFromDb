using System;

namespace SelectFromDb.DbModels
{
	public class Course {
		int Id { get; set; }
		string Navn { get; set; }
	} // end generated class

	public class Student {
		int Id { get; set; }
		string Navn { get; set; }
	} // end generated class

	public class StudentCourse {
		int StudentId { get; set; }
		int CourseId { get; set; }
		DateTime  RegisteredDate { get; set; }
		DateTime  FinishedDate { get; set; }
	}
}
