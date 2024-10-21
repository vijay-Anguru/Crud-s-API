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
    [RoutePrefix("api/session")]
    public class SessionController : ApiController
    {
        [Route("")]
        [HttpGet]
        public IHttpActionResult Index()
        {
            DataBaseHelper dbHelper = new DataBaseHelper();
            DataSet ds = dbHelper.GetDataSet("getAllSessionDetails");
            var data = ds.Tables[0].AsEnumerable().Select(row => new
            {
                EventId = int.Parse(row["EventId"].ToString()),
                SessionId = int.Parse(row["SessionId"].ToString()),
                SessionName = row["SessionName"].ToString(),
                StatusId = long.Parse(row["StatusId"].ToString())
            }).ToList();
            if (data != null)
            {
                return Ok(new { Message = "Data retrieved successfully", Data = data, Total = 0 });
            }
            else
            {
                return Ok(new { Message = "No data found", Data = (object)null, Total = 0 });
             
            }
        }
        [Route("{SessionId:int}")]
        [HttpGet]
        public IHttpActionResult GetById(int SessionId)
        {
            DataBaseHelper dbHelper = new DataBaseHelper();
            Dictionary<string, object> key = new Dictionary<string, object>();
            key["@SessionId"] = SessionId;
            DataSet ds = dbHelper.GetDataSet("getBySessionId", key);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var row = ds.Tables[0].Rows[0];
                var data = new
                {
                    EventId = int.Parse(row["EventId"].ToString()),
                    SessionId = int.Parse(row["SessionId"].ToString()),
                    SessionName = row["SessionName"].ToString(),
                   
                  
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
        public IHttpActionResult Index([FromBody] @session model)
        {
            Dictionary<string, object> key = new Dictionary<string, object>();
            key["@EventId"] = model.EventId;
            key["@SessionId"] = model.SessionId;
            key["@SessionName"] = model.SessionName;
            key["@TransactionType"] = "ADD";
            DataBaseHelper dbHelper = new DataBaseHelper();
            DataSet ds = dbHelper.GetDataSet("SessionDetails", key);
            var data = ds.Tables[0].AsEnumerable().Select(row => new
            {
                StatusId = long.Parse(row["StatusId"].ToString())
            }).ToList();

            if (data != null)
            {
               
                return Ok(new { Message = "Data retrieved successfully", Data = data, Total = 0 });
               
            }
            else
            {
                return Ok(new { Message = "No data found", Data = (object)null, Total = 0 });
               
            }
        }
        [Route("update/{id}")]
        [HttpPut]
        public IHttpActionResult UpdateSession(int id, [FromBody] @session model)
        {
            if (model == null || model.SessionId != id)
            {
                return BadRequest("Invalid session data.");
            }
            Dictionary<string, object> key = new Dictionary<string, object>();
            key["@SessionId"] = id; // ID from the model
            key["@EventId"] = model.EventId;
            key["@SessionName"] = model.SessionName;
            key["@TransactionType"] = "UPDATE";

           
            DataBaseHelper dbHelper = new DataBaseHelper();
            DataSet ds = dbHelper.GetDataSet("SessionDetails", key);

            var data = ds.Tables[0].AsEnumerable().Select(row => new
            {
                StatusId = long.Parse(row["StatusId"].ToString())
            }).ToList();

            if (data != null && data.Count > 0)
            {
                return Ok(new { Message = "Session updated successfully", Data = data, Total = data.Count });
            }
            else
            {
                return Ok(new { Message = "No data found or update failed", Data = (object)null, Total = 0 });
            }
        }
        [Route("{SessionId:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteEvent(int SessionId)
        {
            Dictionary<string, object> key = new Dictionary<string, object>();

          //  key["@EventId"] = EventId;
            key["@SessionId"] = SessionId;
            key["@TransactionType"] = "DELETE";

            DataBaseHelper dbHelper = new DataBaseHelper();
            DataSet ds = dbHelper.GetDataSet("SessionDetails", key);

            var data = ds.Tables[0].AsEnumerable().Select(row => new
            {
                StatusId = long.Parse(row["StatusId"].ToString())
            }).ToList();

            if (data.Count > 0)
            {
                return Ok(new { Message = "Session deleted successfully", Data = data, Total = data.Count });
            }
            else
            {
                return Ok(new { Message = "No event found to delete", Data = (object)null, Total = 0 });
            }
        }

    }
}
