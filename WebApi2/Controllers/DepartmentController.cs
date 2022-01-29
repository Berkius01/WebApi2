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

namespace WebApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DepartmentController(IConfiguration con)
        {
            _configuration = con;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                    select DepartmentId, DepartmentName from dbo.Department";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader MyReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using(SqlCommand myCommand = new SqlCommand(query, myCon))
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
        public JsonResult Post(Department dep)
        {
            string query = @"
                   insert into dbo.Department values 
                    ('"+dep.DepartmentName+@"')";
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
        public JsonResult Put(Department dep)
        {
            string query = @"
                   update dbo.Department set DepartmentName = '"+dep.DepartmentName+@"'
                    where DepertmentId = "+dep.DepartmentId+@"
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
                   delete dbo.Department set DepartmentId = " + id + @"
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

    }
}
