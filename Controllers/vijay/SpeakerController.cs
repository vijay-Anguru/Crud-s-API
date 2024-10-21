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
    [RoutePrefix("api/speaker")]
    public class SpeakerController : ApiController
    {
        [Route("")]
        [HttpGet]
        public IHttpActionResult Index()
        {
            DataBaseHelper dbHelper = new DataBaseHelper();
            DataSet ds = dbHelper.GetDataSet("getAllSpeakerDetails");
            var data = ds.Tables[0].AsEnumerable().Select(row => new
            {
                SessionId = int.Parse(row["SessionId"].ToString()),
                SpeakerId = int.Parse(row["SpeakerId"].ToString()),
                SpeakerName = row["SpeakerName"].ToString(),
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
        [Route("{SpeakerId:int}")]
        [HttpGet]
        public IHttpActionResult GetById(int SpeakerId)
        {
            DataBaseHelper dbHelper = new DataBaseHelper();
            Dictionary<string, object> key = new Dictionary<string, object>();
            key["@SpeakerId"] = SpeakerId;
            DataSet ds = dbHelper.GetDataSet("getBySpeakerId", key);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var row = ds.Tables[0].Rows[0];
                var data = new
                {
                    SessionId = int.Parse(row["SessionId"].ToString()),
                    SpeakerId = int.Parse(row["SpeakerId"].ToString()),
                    SpeakerName = row["SpeakerName"].ToString(),
                   
                  
                };

                return Ok(new { Message = "Data retrieved successfully", Data = data });
            }
            else
            {
                return NotFound(); // Return 404 if no data found
            }
        }
        [Route("")]
        [HttpPost]
        public IHttpActionResult Index([FromBody] @speaker model)
        {

            Dictionary<string, object> key = new Dictionary<string, object>();
            key["@SessionId"] = model.SessionId;
            key["@SpeakerId"] = model.SpeakerId;
            key["@SpeakerName"] = model.SpeakerName;
            key["@TransactionType"] = "ADD";
            DataBaseHelper dbHelper = new DataBaseHelper();
            DataSet ds = dbHelper.GetDataSet("SpeakerDetails", key);
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
        public IHttpActionResult UpdateSpeaker(int id, [FromBody] @speaker model)
        {
            
            if (model == null || model.SpeakerId != id)
            {
                return BadRequest("Invalid speaker data.");
            }

           
            Dictionary<string, object> key = new Dictionary<string, object>();
            key["@SpeakerId"] =id; // ID from the model
            key["@SessionId"] = model.SessionId;
            key["@SpeakerName"] = model.SpeakerName;
            key["@TransactionType"] = "UPDATE";

            // Initialize database helper
            DataBaseHelper dbHelper = new DataBaseHelper();
            DataSet ds = dbHelper.GetDataSet("SpeakerDetails", key);

            var data = ds.Tables[0].AsEnumerable().Select(row => new
            {
                StatusId = long.Parse(row["StatusId"].ToString())
            }).ToList();

            if (data != null && data.Count > 0)
            {
                return Ok(new { Message = "Speaker updated successfully", Data = data, Total = data.Count });
            }
            else
            {
                return Ok(new { Message = "No data found or update failed", Data = (object)null, Total = 0 });
            }
        }
        [Route("{SpeakerId:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteEvent(int SpeakerId)
        {
            Dictionary<string, object> key = new Dictionary<string, object>();
           
            key["@SpeakerId"] = SpeakerId;
            key["@TransactionType"] = "DELETE";

            DataBaseHelper dbHelper = new DataBaseHelper();
            DataSet ds = dbHelper.GetDataSet("SpeakerDetails", key);

            var data = ds.Tables[0].AsEnumerable().Select(row => new
            {
                StatusId = long.Parse(row["StatusId"].ToString())
            }).ToList();

            if (data.Count > 0)
            {
                return Ok(new { Message = "Speaker deleted successfully", Data = data, Total = data.Count });
            }
            else
            {
                return Ok(new { Message = "No event found to delete", Data = (object)null, Total = 0 });
            }
        }


    }
}
