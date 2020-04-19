using APBD03.DTOs.Requests;
using APBD03.DTOs.Responses;
using APBD03.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD03.Services
{
    public interface IStudentsDbService
    {

        IEnumerable<Student> GetStudents();
        
        Student GetStudent(string indexNumber);
        IEnumerable GetStudentSemester(string index);

        EnrollStudentResponse EnrollStudent(EnrollStudentRequest req);

        EnrollStudentResponse PromoteStudents(int semester, string studies);

        public void SaveLogData(string method, string path, string body, string query);

        public Student GetStudentByIndex(string index);

    }
}
