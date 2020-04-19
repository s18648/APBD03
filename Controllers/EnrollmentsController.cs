using APBD03.DTOs.Requests;
using APBD03.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;
using Microsoft.AspNetCore.Http;

namespace APBD03.Controllers
{ 

    [ApiController]
    [Route("api/enrollments")]

    public class EnrollmentsController : ControllerBase
    {
        private readonly IStudentsDbService dbService;
        public EnrollmentsController(IStudentsDbService dbServiceGiven)
        {
            dbService = dbServiceGiven;
        }

        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            EnrollStudentResponse enrollResp = dbService.EnrollStudent(request);

            if(enrollResp == null)
            {
                return BadRequest();
            }

            return this.StatusCode(201, enrollResp);

        }


        [HttpPost("promotions")]
        public IActionResult PromoteStudents(PromoteStudentRequest request)
        {
            PromoteStudentResponse promoteResp = dbService.PromoteStudents(request);

            if(promoteResp == null)
            {
                return BadRequest();
            }

            return this.StatusCode(201, promoteResp);
        }


    }
}
