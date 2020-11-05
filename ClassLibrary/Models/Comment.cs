using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public long Id { get; set; }
        public long CaseId { get; set; }
        public string Description {get; set;}

        public DateTime Created { get; set; }
    }
}
