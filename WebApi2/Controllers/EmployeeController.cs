using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApi2.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace WebApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public EmployeeController(IConfiguration con, IWebHostEnvironment env)
        {
            _configuration = con;
            _env = env;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                    select EmployeeId,EmployeeName,Department,
                    convert(varchar(10),DateOfJoining,120) as DateOfJoining,
                    Photo from dbo.Employee
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader MyReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    MyReader = myCommand.ExecuteReader();
                    table.Load(MyReader);
                    MyReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }
        [HttpPost]
        public JsonResult Post(Employee emp)
        {
            string query = @"
                   insert into dbo.Employee 
                    (EmployeeName,Department,DateOfJoining,Photo)
                    values 
                    (
                    '" + emp.EmployeeName + @"',
                    '" + emp.Department + @"',
                    '" + emp.DateOfJoining + @"',
                    '" + emp.Photo + @"'
                    )";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader MyReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    MyReader = myCommand.ExecuteReader();
                    table.Load(MyReader);
                    MyReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("added succsefully");
        }


        [HttpPut]
        public JsonResult Put(Employee emp)
        {
            string query = @"
                   update dbo.Employee set 
                    EmployeeName = '" + emp.EmployeeName + @"',
                    Department = '" + emp.Department + @"',
                    DateOfJoining = '" + emp.DateOfJoining + @"'        

                    where EmployeeId = " + emp.EmployeeId + @"
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader MyReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    MyReader = myCommand.ExecuteReader();
                    table.Load(MyReader);
                    MyReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("updated succsefully");
        }


        [HttpDelete]
        public JsonResult Delete(int id)
        {
            string query = @"
                   delete dbo.Employee set EmployeeId = " + id + @"
                   ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader MyReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    MyReader = myCommand.ExecuteReader();
                    table.Load(MyReader);
                    MyReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("deleted succsefully");
        }
        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;

                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = _env.ContentRootPath+"/Photos/"+fileName;
                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                return new JsonResult(fileName);
            }
            catch(Exception)
            {
                return new JsonResult("anaonymous.png");
            }

        }
        [Route("GetAllDepartmentName")]
        public JsonResult GetAllDepartmentNames()
        {
            string query = @"
                    select DepartmentName from dbo.Department
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader MyReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    MyReader = myCommand.ExecuteReader();
                    table.Load(MyReader);
                    MyReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }
    }
}
