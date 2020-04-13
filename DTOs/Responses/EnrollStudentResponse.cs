using APBD03.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD03.DTOs.Responses
{
    public class EnrollStudentResponse
    {
        

        public EnrollStudentResponse(Enrollment enroll)
        {
            IdEnrollment = enroll.IdEnrollment;
            semester = enroll.Semester;
            IdStudy = enroll.IdStudy;
            StartDate = enroll.StartDate;

        }

        public int IdEnrollment { get; set; }

        public int semester { get; set; }

        public int IdStudy { get; set; }

        public DateTime StartDate { get; set; }




    }
}
