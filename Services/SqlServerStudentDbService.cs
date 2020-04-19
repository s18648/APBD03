using APBD03.DTOs.Requests;
using APBD03.DTOs.Responses;
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




        public EnrollStudentResponse EnrollStudent(EnrollStudentRequest req)
        {
            
            using (var con = new SqlConnection(@"Data Source=db-mssql;Initial Catalog=s18648;Integrated Security=True"))
            {
                con.Open();
                int idStudy = 1;
                int IDenroll = 1;

                var tran = con.BeginTransaction();


                using (var com = new SqlCommand())
                {
                    com.Connection = con;
                    com.Transaction = tran;
                    com.CommandText = "@SELECT * FROM Studies s WHERE s.Name = @Name";
                    com.Parameters.AddWithValue("@Name", req.Studies);


                    using (var read = com.ExecuteReader())
                    {
                        if(!read.Read())
                        {
                            read.Close();
                            tran.Rollback();
                            con.Close();
                            return null;
                        }

                        idStudy = int.Parse(read["IdStudy"].ToString());
                    }

                }


                using(var com = new SqlCommand())
                {
                    com.Connection = con;
                    com.Transaction = tran;
                    com.CommandText = "@SELECT * FROM Enrollment e WHERE e.IdStudy = @IDStudy AND e.Semester=1";
                    com.Parameters.AddWithValue("@IDStudy", idStudy);


                    using (var read = com.ExecuteReader())
                    {
                        if (read.Read())
                        {
                            IDenroll = int.Parse(read["IdEnrollment"].ToString());
                        } else
                        {
                            read.Close();

                            using (var com2 = new SqlCommand())
                            {
                                com2.Connection = con;
                                com2.Transaction = tran;
                                com2.CommandText = "SELECT MAX(e.IdEnrollment) as IdEnrollment FROM Enrollment e";


                                using (var read2 = com2.ExecuteReader())
                                {
                                    if(read2.Read())
                                    {
                                        IDenroll = (int.Parse(read2["IdEnrollment"].ToString())) + 1;

                                    }
                                    read2.Close();
                                }

                            }


                        }
                    }

                }
                



            }
            return null;



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
                                            "Where s.IndexNumber = @indexNumber";
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


        public EnrollStudentResponse PromoteStudents(int semester, string studies)
        {
            throw new NotImplementedException();
        }

        public Student GetStudentByIndex(string index)
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
                                            "Where s.IndexNumber = @indexNumber";
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
