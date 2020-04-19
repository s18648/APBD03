using APBD03.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD03.Middlewares
{
    public class CustomLoggingMiddleware


    {
        private readonly RequestDelegate _next;

        public CustomLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task InovokeAsync(HttpContext context, IStudentsDbService serv)
        {

            if (context.Request != null)
            {
                string path = context.Request.Path;
                string method = context.Request.Method;
                string queryString = context.Request.QueryString.ToString();
                string bodyStr = "";

                using (StreamReader reader = new StreamReader(
                    context.Request.Body, Encoding.UTF8, true, 1024, true))
                {

                    bodyStr = await reader.ReadToEndAsync();


                    

                }

                if (!File.Exists("C:\\Users\\Nika\\source\\repos\\APBD03\\APBD03"))
                {
                    File.Create("C:\\Users\\Nika\\source\\repos\\APBD03\\APBD03").Dispose();
                }

                StreamWriter sw = File.AppendText("C:\\Users\\Nika\\source\\repos\\APBD03\\APBD03");

                sw.WriteLine("Path: \n" + path + "; \n Query String: \n" + queryString + ";\n Method: \n" + method + ";\n Body Parameters: \n" + bodyStr);

                sw.Close();


            }

            await _next(context);
        }
    }
}
