using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeSauce
{
    class Node
    {
        public int Data;
        public bool Color; //red - false; black - true
        public Node Left;
        public Node Right;
        public Node Parent;
        public int Count;
        public Node(Node left, int data, bool color, Node right, Node parent, int count)
        {
            Left = left;
            Data = data;
            Color = color;
            Right = right;
            Parent = parent;
            Count = count;
        }
    }

}
