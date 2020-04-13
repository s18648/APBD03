using APBD03.DTOs.Requests;
using APBD03.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace APBD03.Services
{
    public class SqlServerStudentDbService : IStudentsDbService
    {









        public void EnrollStudent(EnrollStudentRequest req)
        {
            throw new NotImplementedException();
        }








        //i think its GetStudentSemester()

        public IEnumerable<Enrollment> GetEnrollments(string indexNumber)
        {
            throw new NotImplementedException();
        }













        public IEnumerable GetStudentSemester(string index)
        {
            var listOfSemesters = new List<string>();

            using (var connection = new SqlConnection(@"Data Source=db-mssql;Initial Catalog=s18648;Integrated Security=True"))
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "Select e.Semester From Student s inner join Enrollment e On s.IdEnrollment = e.IdEnrollment Where s.IndexNumber = @index";
                    command.Parameters.AddWithValue("@index", index);
                    connection.Open();
                    var read = command.ExecuteReader();
                    while (read.Read())
                    {
                        var semester = read["Semester"].ToString();
                        listOfSemesters.Add(semester);
                    }
                }
            }

            return listOfSemesters;

        }


        public IEnumerable<Student> GetStudents() 
        {
            var students = new List<Student>();
            using (var sqlConnection = new SqlConnection(@"Data Source=db-mssql;Initial Catalog=s18648;Integrated Security=True"))
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = sqlConnection;
                    command.CommandText = "select s.FirstName, s.LastName, s.BirthDate, st.Name as Studies, e.Semester " +
                                            "from Student s " +
                                            "join Enrollment e on e.IdEnrollment = s.IdEnrollment " +
                                            "join Studies st on st.IdStudy = e.IdStudy; ";
                    sqlConnection.Open();
                    var response = command.ExecuteReader();
                    while (response.Read())
                    {


                        var st = new Student
                        {
                            FirstName = response["FirstName"].ToString(),
                            LastName = response["LastName"].ToString(),
                            Studies = response["Studies"].ToString(),
                            BirthDate = DateTime.Parse(response["BirthDate"].ToString()),
                            Semester = int.Parse(response["Semester"].ToString())
                        };

                        students.Add(st);
                    }
                }
            }
            return students;
        }











        public void PromoteStudents(int semester, string studies)
        {
            throw new NotImplementedException();
        }























        Student IStudentsDbService.GetStudent(string indexNumber)
        {
            using (var sqlConnection = new SqlConnection(@"Data Source=db-mssql;Initial Catalog=s18648;Integrated Security=True"))
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = sqlConnection;
                    command.CommandText = "select s.FirstName, s.LastName, s.BirthDate, st.Name as Studies, e.Semester " +
                                            "from Student s " +
                                            "join Enrollment e on e.IdEnrollment = s.IdEnrollment " +
                                            "join Studies st on st.IdStudy = e.IdStudy;+" +
                                            "Where s.IndexNumber = @index";
                    sqlConnection.Open();
                    var response = command.ExecuteReader();

                    response.Read();
                    

                        var st = new Student
                        {
                            FirstName = response["FirstName"].ToString(),
                            LastName = response["LastName"].ToString(),
                            Studies = response["Studies"].ToString(),
                            BirthDate = DateTime.Parse(response["BirthDate"].ToString()),
                            Semester = int.Parse(response["Semester"].ToString())
                        };

                   
                    return st;
                }

            }

        }
    }
}
