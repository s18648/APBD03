﻿using APBD03.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD03.DTOs.Responses
{
    public class EnrollStudentResponse
    {
        


        public int IdEnrollment { get; set; }

        public int Semester { get; set; }

        public int IdStudy { get; set; }

        public DateTime StartDate { get; set; }




    }
}
