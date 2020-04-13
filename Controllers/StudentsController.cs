using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using APBD03.Models;
using APBD03.Services;

namespace APBD03.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {


        private readonly IStudentsDbService dbService;
        public StudentsController(IStudentsDbService dbServiceGiven)
        {
            dbService = dbServiceGiven;
        }


        [HttpGet]
        public IActionResult GetStudents()  
        {
            var students = dbService.GetStudents();
            return Ok(students);
        }

        [HttpGet("{index}")]
        public IActionResult GetStudentSemester(string index)
        {
            var listOfSemesters = dbService.GetStudentSemester(index);

            return Ok(listOfSemesters);
        }

        [HttpGet("{index}")]
        public IActionResult GetStudent(string index)
        {
            var student = dbService.GetStudent(index);
            if(student != null)
            {
                return Ok(student);
            } else
            {
                return NotFound("Student was not found on the student list");
            }
        }





















    }


    




}
