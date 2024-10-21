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
    [RoutePrefix("api/event")]
    public class EventController : ApiController
    {
        [Route("")]
        [HttpGet]
        public IHttpActionResult Index()
        {
            DataBaseHelper dbHelper = new DataBaseHelper();
            DataSet ds = dbHelper.GetDataSet("getAllEventDetails");
            var data = ds.Tables[0].AsEnumerable().Select(row => new
            {
                EventId = int.Parse(row["EventId"].ToString()),
                EventName = row["EventName"].ToString(),
                location = row["location"].ToString(),
                Description = row["Description"].ToString(),
                Organizer = row["Organizer"].ToString(),
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
        [Route("{EventId:int}")]
        [HttpGet]
        public IHttpActionResult GetById(int EventId)
        {
            DataBaseHelper dbHelper = new DataBaseHelper();
            Dictionary<string, object> key = new Dictionary<string, object>();
            key["@EventId"] = EventId;
            DataSet ds = dbHelper.GetDataSet("getByEventId",key);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var row = ds.Tables[0].Rows[0];
                var data = new
                {
                    EventId = int.Parse(row["EventId"].ToString()),
                    EventName = row["EventName"].ToString(),
                    location = row["location"].ToString(),
                    Description = row["Description"].ToString(),
                    Organizer = row["Organizer"].ToString(),
                  
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
        public IHttpActionResult Index([FromBody] @event model)
        {

            Dictionary<string, object> key = new Dictionary<string, object>();
            key["@EventId"] = model.EventId;
            key["@EventName"] = model.EventName;
            key["@location"] = model.location;
            key["@Organizer"] = model.Organizer;
            key["@Description"] = model.Description;
            key["@TransactionType"] = "ADD";
            DataBaseHelper dbHelper = new DataBaseHelper();
            DataSet ds = dbHelper.GetDataSet("EventDetails", key);
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
        public IHttpActionResult UpdateEvent(int id, [FromBody] @event model)
        {
           
            if (model == null || model.EventId != id)
            {
                return BadRequest("Invalid event data.");
            }

            
            Dictionary<string, object> key = new Dictionary<string, object>();
            key["@EventId"] =id; // ID from the model
            key["@EventName"] = model.EventName;
            key["@location"] = model.location;
            key["@Organizer"] = model.Organizer;
            key["@Description"] = model.Description;
            key["@TransactionType"] = "UPDATE";

            
            DataBaseHelper dbHelper = new DataBaseHelper();
            DataSet ds = dbHelper.GetDataSet("EventDetails", key);

            var data = ds.Tables[0].AsEnumerable().Select(row => new
            {
                StatusId = long.Parse(row["StatusId"].ToString())
            }).ToList();

            if (data != null && data.Count > 0)
            {
                return Ok(new { Message = "Event updated successfully", Data = data, Total = data.Count });
            }
            else
            {
                return Ok(new { Message = "No data found or update failed", Data = (object)null, Total = 0 });
            }
        }
        [Route("{EventId:int}")] 
        [HttpDelete]
        public IHttpActionResult DeleteEvent(int EventId)
        {
            Dictionary<string, object> key = new Dictionary<string, object>();
            key["@EventId"] = EventId;
            key["@TransactionType"] = "DELETE";

            DataBaseHelper dbHelper = new DataBaseHelper();
            DataSet ds = dbHelper.GetDataSet("EventDetails", key);

            var data = ds.Tables[0].AsEnumerable().Select(row => new
            {
                StatusId = long.Parse(row["StatusId"].ToString())
            }).ToList();

            if (data.Count > 0)
            {
                return Ok(new { Message = "Event deleted successfully", Data = data, Total = data.Count });
            }
            else
            {
                return Ok(new { Message = "No event found to delete", Data = (object)null, Total = 0 });
            }
        }




    }
}
