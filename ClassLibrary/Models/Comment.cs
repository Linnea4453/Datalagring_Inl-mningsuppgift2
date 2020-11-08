using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SQLitePCL;

namespace ClassLibrary.Models
{
    public class Comment
    {
        public Comment()
        {
                
        }
        public Comment(long id, long caseId, string description, DateTime created)
        {
            Id = id;
            CaseId = caseId;
            Description = description;
            Created = created;
        }
        [JsonProperty(propertyName: "id")]
        public long Id { get; set; }
        [JsonProperty(propertyName: "caseid")]
        public long CaseId { get; set; }
        [JsonProperty(propertyName: "description")]
        public string Description {get; set;}
        [JsonProperty(propertyName: "created")]
        public DateTime Created { get; set; }
    }
}
