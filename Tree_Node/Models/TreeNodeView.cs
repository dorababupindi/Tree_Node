using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tree_Node.Models
{
    public class TreeNodeView
    {
        public List<Parent> Parents { get; set; }
        public List<List<string>> ChildNames { get; set; }
    }

    public class Parent
    {
        public string Name { get; set; }
    }

    public class child
    {
        public List<EachChild> ChildNames { get; set; }
    }
    public class EachChild
    {
        public string Name { get; set; }
    }

    public class childs 
    {
        public List<string> Childs { get; set; }
    }
}