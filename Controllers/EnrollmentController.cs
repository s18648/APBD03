using APBD03.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace APBD03.Controllers
{ 

    [ApiController]
    [Route("api/enrollments")]

    public class EnrollmentController
    {
        private readonly IStudentsDbService dbService;
        public EnrollmentController(IStudentsDbService dbServiceGiven)
        {
            dbService = dbServiceGiven;
        }





    }
}
