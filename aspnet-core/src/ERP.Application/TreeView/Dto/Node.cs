using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.TreeView.Dto
{
    public class Node
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? ParentId { get; set; }
        public string Summary { get; set; }
        public long? Type { get; set; }
        public long? Status { get; set; }
        public string Discription { get; set; }
        public long? Assignee_Id { get; set; }
        public long? Reporter_Id { get; set; }
        public DateTime? Due_Date { get; set; }
        public DateTime? Created { get; set; }
        public long? Priority { get; set; }
        public Decimal? Estimate { get; set; }
        public string Assignee { get; set; }
        public string Reporter { get; set; }
    }
    public class TreeNode
    {
        public List<Node> NodeFlat { get; set; }
    }
}
