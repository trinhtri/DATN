using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.TreeView.Dto
{
   public class GetTreeInputDto
    {
        public List<long> ListProjectId { get; set; }
        public List<long> ListStatusId { get; set; }

        public List<long> ListAssignId { get; set; }
        public List<long> ListTypeId { get; set; }


        public string Filter { get; set; }
    }
}
