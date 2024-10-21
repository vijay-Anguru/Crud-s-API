using Api.DAL;
using Api.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Api.Controllers.vijay
{
    [RoutePrefix("api/master")]
    public class MasterAllController : ApiController
    {
        [Route("employee")]
        [HttpGet]
        public IHttpActionResult Index()
        {
            DataBaseHelper dbHelper = new DataBaseHelper();
            DataSet ds = dbHelper.GetDataSet("getallEmployees");
            var data = ds.Tables[0].AsEnumerable().Select(row => new
            {
                EmployeeId = int.Parse(row["EmployeeId"].ToString()),
                ClientId = int.Parse(row["ClientId"].ToString()),
                EmployeeCode = row["EmployeeCode"].ToString(),
                FirstName = row["FirstName"].ToString(),
                MiddleName = row["MiddleName"].ToString(),
                LastName = row["LastName"].ToString(),
                ContactNo = row["ContactNo"].ToString(),
                EmailId = row["EmailId"].ToString(),
                IsActive = bool.Parse(row["IsActive"].ToString()),
                IsDeleted = bool.Parse(row["IsDeleted"].ToString()),
                EmployeeTypeId = row["EmployeeTypeId"].ToString()
            }).ToList();
            int total = ds.Tables[1].AsEnumerable().Select(x => int.Parse(x["total"].ToString())).FirstOrDefault();
            if (data != null)
            {
                return Ok(new { Message = "Data retrieved successfully", Data = data, Total = total });
            }
            else
            {
                return Ok(new { Message = "No data found", Data = (object)null, Total = 0 });
            }
        }
        [Route("department")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            DataBaseHelper dbHelper = new DataBaseHelper();
            DataSet ds = dbHelper.GetDataSet("getalldepartments");
            var data = ds.Tables[0].AsEnumerable().Select(row => new
            {
                DepartmentId = int.Parse(row["DepartmentId"].ToString()),
                EnterpriseMasterId = int.Parse(row["EnterpriseMasterId"].ToString()),
                DepartmentName = row["DepartmentName"].ToString(),
                IsActive = bool.Parse(row["IsActive"].ToString()),
                IsDeleted = bool.Parse(row["IsDeleted"].ToString())
            }).ToList();
            int Totaldepartment = ds.Tables[1].AsEnumerable().Select(x => int.Parse(x["Totaldepartment"].ToString())).FirstOrDefault();
            if (data != null)
            {
                return Ok(new { Message = "Data retrieved successfully", Data = data, Total = Totaldepartment });
            }
            else
            {
                return Ok(new { Message = "No data found", Data = (object)null, Total = 0 });
            }
        }

        [Route("folder")]
        [HttpGet]
        public IHttpActionResult Getall()
        {
            DataBaseHelper dbHelper = new DataBaseHelper();
            DataSet ds = dbHelper.GetDataSet("GetAllFolders");
            var data = ds.Tables[0].AsEnumerable().Select(row => new
            {
                FolderId = int.Parse(row["FolderId"].ToString()),
                EnterpriseMasterId = int.Parse(row["EnterpriseMasterId"].ToString()),
                FolderName = row["FolderName"].ToString(),
                IsActive = bool.Parse(row["IsActive"].ToString()),
                IsDeleted = bool.Parse(row["IsDeleted"].ToString())
            }).ToList();
            int total = ds.Tables[1].AsEnumerable().Select(x => int.Parse(x["total"].ToString())).FirstOrDefault();
            if (data != null)
            {
                return Ok(new { Message = "Data retrieved successfully", Data = data, Total = total });
            }
            else
            {
                return Ok(new { Message = "No data found", Data = (object)null, Total = 0 });
            }
        }
        [Route("subfolder")]
        [HttpGet]
        public IHttpActionResult Getsubfolder()
        {
            DataBaseHelper dbHelper = new DataBaseHelper();
            DataSet ds = dbHelper.GetDataSet("GetAllSubFolders");
            var data = ds.Tables[0].AsEnumerable().Select(row => new
            {
                SubFolderId = int.Parse(row["SubFolderId"].ToString()),
                FolderId = int.Parse(row["FolderId"].ToString()),
                EnterpriseMasterId = int.Parse(row["EnterpriseMasterId"].ToString()),
                SubFolderName = row["SubFolderName"].ToString(),
                IsActive = bool.Parse(row["IsActive"].ToString()),
                IsDeleted = bool.Parse(row["IsDeleted"].ToString())
            }).ToList();
            int total = ds.Tables[1].AsEnumerable().Select(x => int.Parse(x["total"].ToString())).FirstOrDefault();
            if (data != null)
            {
                return Ok(new { Message = "Data retrieved successfully", Data = data, Total = total });
            }
            else
            {
                return Ok(new { Message = "No data found", Data = (object)null, Total = 0 });
            }
        }
        [Route("regulation")]
        [HttpGet]
        public IHttpActionResult Getregulation()
        {
            DataBaseHelper dbHelper = new DataBaseHelper();
            DataSet ds = dbHelper.GetDataSet("getallregulation");
            var data = ds.Tables[0].AsEnumerable().Select(row => new
            {
                RegulationById = int.Parse(row["RegulationById"].ToString()),
                EnterpriseMasterId = int.Parse(row["EnterpriseMasterId"].ToString()),
                RegulationByName = row["RegulationByName"].ToString(),
                IsActive = bool.Parse(row["IsActive"].ToString()),
                IsDeleted = bool.Parse(row["IsDeleted"].ToString())
            }).ToList();
            int TotalPolicy = ds.Tables[1].AsEnumerable().Select(x => int.Parse(x["TotalPolicy"].ToString())).FirstOrDefault();
            if (data != null)
            {
                return Ok(new { Message = "Data retrieved successfully", Data = data, Total = 0 });
            }
            else
            {
                return Ok(new { Message = "No data found", Data = (object)null, Total = 0 });
            }
        }
        [Route("policy")]
        [HttpGet]
        public IHttpActionResult GetPolicy()
        {
            DataBaseHelper dbHelper = new DataBaseHelper();
            DataSet ds = dbHelper.GetDataSet("getallpolicy");
            var data = ds.Tables[0].AsEnumerable().Select(row => new
            {
                PolicyId = int.Parse(row["PolicyId"].ToString()),
                PolicyName = row["PolicyName"].ToString(),
                Description = row["Description"].ToString(),
                DepartmentId = int.Parse(row["DepartmentId"].ToString()),
                FolderId = int.Parse(row["FolderId"].ToString()),
                SubFolderId = int.Parse(row["SubFolderId"].ToString()),
                ReviewerId = int.Parse(row["ReviewerId"].ToString()),
                RegulationById = int.Parse(row["RegulationById"].ToString()),
                PolicyWrittenBy = int.Parse(row["PolicyWrittenBy"].ToString()),
                CreatedDate = Convert.ToDateTime(row["CreatedDate"].ToString()),
            }).ToList();
            int TotalPolicy = ds.Tables[1].AsEnumerable().Select(x => int.Parse(x["TotalPolicy"].ToString())).FirstOrDefault();
            if (data != null)
            {
                return Ok(new { Message = "Data retrieved successfully", Data = data, Total = TotalPolicy });
            }
            else
            {
                return Ok(new { Message = "No data found", Data = (object)null, Total = 0 });
            }
        }
        [Route("{PolicyId:int}")]
        [HttpGet]
        public IHttpActionResult GetById(int PolicyId)
        {
            DataBaseHelper dbHelper = new DataBaseHelper();
            Dictionary<string, object> key = new Dictionary<string, object>();
            key["@PolicyId"] = PolicyId;
            DataSet ds = dbHelper.GetDataSet("getByPolicyId", key);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var row = ds.Tables[0].Rows[0];
                var data = new
                {
                    PolicyId = int.Parse(row["PolicyId"].ToString()),
                    PolicyName = row["PolicyName"].ToString(),
                    Description = row["Description"].ToString(),
                    DepartmentId = int.Parse(row["DepartmentId"].ToString()),
                    FolderId = int.Parse(row["FolderId"].ToString()),
                    SubFolderId = int.Parse(row["SubFolderId"].ToString()),
                    ReviewerId = int.Parse(row["ReviewerId"].ToString()),
                    RegulationById = int.Parse(row["RegulationById"].ToString()),
                    PolicyWrittenBy = int.Parse(row["PolicyWrittenBy"].ToString()),
                    CreatedDate = Convert.ToDateTime(row["CreatedDate"].ToString()),

                };

                return Ok(new { Message = "Data retrieved successfully", Data = data });
            }
            else
            {
                return NotFound();
            }
        }
        [Route("")]
        [HttpPost]
        public IHttpActionResult Index([FromBody] @policy model)
        {

            Dictionary<string, object> key = new Dictionary<string, object>();
            key["@PolicyId"] = model.PolicyId;
            key["@ReferenceNo"] = model.ReferenceNo;
            key["@PolicyName"] = model.PolicyName;
            key["@Description"] = model.Description;
            key["@DepartmentId"] = model.DepartmentId;
            key["@FolderId"] = model.FolderId;
            key["@SubFolderId"] = model.SubFolderId;
            key["@ReviewerId"] = model.ReviewerId;
            key["@RegulationById"] = model.RegulationById;
            key["@PolicyWrittenBy"] = model.PolicyWrittenBy;
            key["@CreatedDate "] = model.CreatedDate;
            key["@TransactionType"] = "ADD";
            DataBaseHelper dbHelper = new DataBaseHelper();
            DataSet ds = dbHelper.GetDataSet("PolicyDetails", key);
            var data = ds.Tables[0].AsEnumerable().Select(row => new
            {
                StatusId = long.Parse(row["StatusId"].ToString())
            }).ToList();

            if (data != null)
            {
                return Ok(new { Message = "Data saved successfully", Data = data, Total = 0 });
            }
            else
            {
                return Ok(new { Message = "No data found", Data = (object)null, Total = 0 });
            }

        }
        [Route("update/{id}")]
        [HttpPut]
        public IHttpActionResult UpdatePolicy(int id, [FromBody] @policy model)
        {
            if (model == null || model.PolicyId != id)
            {
                return BadRequest("Invalid Policy data.");
            }
            Dictionary<string, object> key = new Dictionary<string, object>();
            key["@PolicyId"] = id; 
            key["@ReferenceNo"] = model.ReferenceNo;
            key["@PolicyName"] = model.PolicyName;
            key["@Description"] = model.Description;
            key["@DepartmentId"] = model.DepartmentId;
            key["@FolderId"] = model.FolderId;
            key["@SubFolderId"] = model.SubFolderId;
            key["@ReviewerId"] = model.ReviewerId;
            key["@RegulationById"] = model.RegulationById;
            key["@PolicyWrittenBy"] = model.PolicyWrittenBy;
            key["@CreatedDate "] = model.CreatedDate;
            key["@TransactionType"] = "UPDATE";

          
            DataBaseHelper dbHelper = new DataBaseHelper();
            DataSet ds = dbHelper.GetDataSet("PolicyDetails", key);

            var data = ds.Tables[0].AsEnumerable().Select(row => new
            {
                StatusId = long.Parse(row["StatusId"].ToString())
            }).ToList();

            if (data != null && data.Count > 0)
            {
                return Ok(new { Message = "Policy Details updated successfully", Data = data, Total = data.Count });
            }
            else
            {
                return Ok(new { Message = "No data found or update failed", Data = (object)null, Total = 0 });
            }
        }
        [Route("{PolicyId:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteEvent(int PolicyId)
        {
            Dictionary<string, object> key = new Dictionary<string, object>();

            key["@PolicyId"] = PolicyId;
            key["@TransactionType"] = "DELETE";

            DataBaseHelper dbHelper = new DataBaseHelper();
            DataSet ds = dbHelper.GetDataSet("PolicyDetails", key);

            var data = ds.Tables[0].AsEnumerable().Select(row => new
            {
                StatusId = long.Parse(row["StatusId"].ToString())
            }).ToList();

            if (data.Count > 0)
            {
                return Ok(new { Message = "Policy Detail deleted successfully", Data = data, Total = data.Count });
            }
            else
            {
                return Ok(new { Message = "No event found to delete", Data = (object)null, Total = 0 });
            }
        }
    }
}

