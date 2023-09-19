using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tree_Node.Models
{
    public class TreeNodeMaster
    {
        public int NodeId { get; set; }
        public string NodeName { get; set; }
        public int ParentNodeId { get; set; }
        public bool IsActive { get; set; }
    }
}