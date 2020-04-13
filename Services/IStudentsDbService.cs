using APBD03.DTOs.Requests;
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

        void EnrollStudent(EnrollStudentRequest req);

        void PromoteStudents(int semester, string studies);

    }
}
