using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Issue.Dto
{
   public class ScaleStatusIssueOfSprintForChart
    {
        public int ScaleOpen { get; set; }
        public int ScaleInProgress { get; set; }
        public int ScaleResolved { get; set; }
        public int ScaleCompeleted { get; set; }
        public int ScaleReOpened { get; set; }
    }
}
