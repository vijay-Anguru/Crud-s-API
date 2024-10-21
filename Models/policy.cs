using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class @policy
    {
        public int PolicyId { get; set; }
        public string ReferenceNo { get; set; }
        public string PolicyName { get; set; }
        public string Description { get; set; }
        public string Organizer { get; set; }

        public int DepartmentId { get; set; }
        public int FolderId { get; set; }
        public int SubFolderId { get; set; }
        public int ReviewerId { get; set; }
        public int RegulationById { get; set; }
        public int PolicyWrittenBy { get; set; }
        public DateTime CreatedDate { get; set; }
    


    }
}