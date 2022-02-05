using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeViewDemo.Models
{
    public class Node
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public IList<Node> Children { get; set; }

        public Node()
        {
            Name = Id;
        }
    }
}
