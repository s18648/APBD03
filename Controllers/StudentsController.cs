using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using APBD03.Models;

namespace APBD03.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        

        [HttpGet]
        public IActionResult GetStudent()  //here should be string orderBy
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
                        //var st = new Student();
                        //st.FirstName = response["FirstName"].ToString();
                        //st.LastName = response["LastName"].ToString();
                        //st.Studies = response["Studies"].ToString();
                        //st.BirthDate = DateTime.Parse(response["BirthDate"].ToString());
                        //st.Semester = int.Parse(response["Semester"].ToString());

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
            return Ok(students);
        }
        /*
        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            if (id == 1)
            {
                return Ok("Kowalski");
            }
            else if (id == 2)
            {
                return Ok("Malewski");
            }
            return NotFound("Cannot find the student");
        }
        */





        //[HttpPost]
        //public IActionResult CreateStudent(Student student)
        //{
        //    student.IndexNumber = $"s{new Random().Next(1, 20000)}";
        //    // _dbService.GetStudents().ToList().Add(student);
        //    return Ok(student);
        //}

        //[HttpPut("{id}")]
        //public IActionResult UpdateStudent(Student student, int id)
        //{
        //    if(student.idStudent != id)
        //    {
        //         return NotFound("Student Not Found");
        //    }
        //    // updating object 
        //    // student.FirstName = "James";
        //    // _dbService.GetStudents().ToList().Insert(id, student);
        //    return Ok("Update completed");
        //}

        //[HttpDelete("{id}")]
        //public IActionResult DeleteStudent(int id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound("Student Not Found");
        //    }
        //    // deleting object 
        //    //_dbService.GetStudents().ToList().RemoveAt(id);
        //    return Ok("Delete completed");
        //}



        [HttpGet("{index}")]
        public IActionResult GetStudent(string index)
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

            return Ok(listOfSemesters);

        }


















    }


    




}
