using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeSauce
{
    class Tree
    {
        private Node _nil = new Node(null, default, true, null, null, 1);
        private Node _root;

        public Node Root
        {
            get
            {
                return _root;
            }
        }
        public Tree()
        {

        }
        public void NilMaker()
        {
            _nil.Left = _nil;
            _nil.Parent = _nil;
            _nil.Right = _nil;
        }
        public void Insert(string z)
        {
            Node n = new Node(_nil, StringToInt(z), false, _nil, _nil, 1);
            InsertNode(n);
        }
        public void Delete(string z)
        {
            int p = StringToInt(z);
            Node n = FindNode(p);
            DeleteNode(n);
        }
        public void PrintMinimum()
        {
            Key k = IntToKey(FindMinimum());
            Console.WriteLine(k.Field1 + "." + k.Field2 + "." + k.Field3);
        }
        public bool Find(string k)
        {
            int p = StringToInt(k);
            Node n = FindNode(p);
            if (n.Data != 0)
                return true;
            return false;
        }
        public void Print()
        {
            PrintHelp(_root);
        }
        public void LRPrint()
        {
            LRPrintHelp(_root);
        }
        public void RLPrint()
        {
            RLPrintHelp(_root);
        }
        public void NLRPrint()
        {
            NLRPrintHelp(_root);
        }
        public void Draw()
        {
            DrawHelp(_root, 0);
        }
        public void Clean()
        {
            while (_root != _nil)
            {
                DeleteNode(_root);
            }
        }
        private Key IntToKey(int n)
        {
            Key z = new Key("00", "00", "00");
            z.Field1 = Convert.ToString(n / 10000);
            z.Field2 = Convert.ToString((n % 10000) / 100);
            z.Field3 = Convert.ToString(n % 100);
            if (z.Field1.Length < 2)
                z.Field1 = "0" + z.Field1;
            if (z.Field2.Length < 2)
                z.Field2 = "0" + z.Field2;
            if (z.Field3.Length < 2)
                z.Field3 = "0" + z.Field3;
            return z;
        }
        private int KeyToInt(Key z)
        {
            int n = 0;
            n = Convert.ToInt32(z.Field1) * 10000 + Convert.ToInt32(z.Field2) * 100 + Convert.ToInt32(z.Field3);
            return n;
        }
        private Key StringToKey(string k)
        {
            Key z = default;
            z.Field1 = Convert.ToString(k[7] * 10 - 480 + k[6] - 48);
            z.Field1 = Convert.ToString(k[4] * 10 - 480 + k[3] - 48);
            z.Field1 = Convert.ToString(k[1] * 10 - 480 + k[0] - 48);
            return z;
        }
        private int StringToInt(string k)
        {
            int n = (k[0] - 48) * 100000 + (k[1] - 48) * 10000 +
                (k[3] - 48) * 1000 + (k[4] - 48) * 100 +
                (k[6] - 48) * 10 + (k[7] - 48);
            return n;
        }
        private void LeftRotate(Node x)
        {
            Node y = x.Right;
            x.Right = y.Left;

            if (y.Left != _nil)
                y.Left.Parent = x;
            y.Parent = x.Parent;

            if (x.Parent == _nil)
                _root = y;
            else if (x == x.Parent.Left)
                x.Parent.Left = y;
            else
                x.Parent.Right = y;

            y.Left = x;
            x.Parent = y;
        }
        private void RightRotate(Node y)
        {
            Node x = y.Left;
            y.Left = x.Right;

            if (x.Right != _nil)
                x.Right.Parent = y;

            x.Parent = y.Parent;

            if (y.Parent == _nil)
                _root = x;
            else if (y == y.Parent.Right)
                y.Parent.Right = x;
            else
                y.Parent.Left = x;

            x.Right = y;
            y.Parent = x;
        }
        private void InsertNode(Node z)
        {
            if (_root != null)
            {
                Node y = _nil;
                Node x = _root;
                while (x != _nil)
                {
                    if (z.Data == y.Data)
                    {
                        y.Count += 1;
                        return;
                    }
                    y = x;
                    if (z.Data < x.Data)
                        x = x.Left;
                    else if (z.Data > x.Data)
                        x = x.Right;
                }
                z.Parent = y;
                if (y == _nil)
                    _root = z;
                else if (z.Data < y.Data)
                    y.Left = z;
                else
                    y.Right = z;
                z.Left = _nil;
                z.Right = _nil;
                z.Color = false;
                InsertFixup(z);
            }
            else
            {
                _root = z;
                _root.Color = true;
            }
        }
        private void InsertFixup(Node z)
        {
            while (z != _root && z.Parent.Color == false)
            {
                if (z.Parent == z.Parent.Parent.Left)
                {
                    Node y = z.Parent.Parent.Right;
                    if (y != null && y.Color == false)
                    {
                        z.Parent.Color = true;
                        y.Color = true;
                        z.Parent.Parent.Color = false;
                        z = z.Parent.Parent;
                    }
                    else
                    {
                        if (z == z.Parent.Right)
                        {
                            z = z.Parent;
                            LeftRotate(z);
                        }
                        z.Parent.Color = true;
                        z.Parent.Parent.Color = false;
                        RightRotate(z.Parent.Parent);
                    }
                }
                else
                {
                    Node y = z.Parent.Parent.Left;
                    if (y != null && y.Color == false)
                    {
                        z.Parent.Color = true;
                        y.Color = true;
                        z.Parent.Parent.Color = false;
                        z = z.Parent.Parent;
                    }
                    else
                    {
                        if (z == z.Parent.Left)
                        {
                            z = z.Parent;
                            RightRotate(z);
                        }
                        z.Parent.Color = true;
                        z.Parent.Parent.Color = false;
                        LeftRotate(z.Parent.Parent);
                    }
                }
            }
            _root.Color = true;
        }
        private void Transplant(Node u, Node v)
        {
            if (u.Parent == _nil)
                _root = v;
            else if (u == u.Parent.Left)
                u.Parent.Left = v;
            else
                u.Parent.Right = v;
            v.Parent = u.Parent;
        }
        private void DeleteNode(Node z)
        {
            if (z.Count > 1)
            {
                z.Count -= 1;
            }
            else
            {
                Node y = z;
                bool yColor = y.Color;
                Node x;
                if (z.Left == _nil)
                {
                    x = z.Right;
                    Transplant(z, z.Right);
                }
                else if (z.Right == _nil)
                {
                    x = z.Left;
                    Transplant(z, z.Left);
                }
                else
                {
                    y = Minimum(z.Right);
                    yColor = y.Color;
                    x = y.Right;
                    if (y.Parent == z)
                    {
                        x.Parent = y;
                    }
                    else
                    {
                        Transplant(y, y.Right);
                        y.Right = z.Right;
                        y.Right.Parent = y;
                    }
                    Transplant(z, y);
                    y.Left = z.Left;
                    y.Left.Parent = y;
                    y.Color = z.Color;
                }
                if (yColor == true)
                    DeliteFixup(x);
            }
        }
        private void DeliteFixup(Node x)
        {
            while (x != _root && x.Color == true)
            {
                if (x == x.Parent.Left)
                {
                    Node w = x.Parent.Right;
                    if (w.Color == false)
                    {
                        w.Color = true;
                        x.Parent.Color = false;
                        LeftRotate(x.Parent);
                        w = x.Parent.Right;
                    }
                    if (w.Left.Color == true && w.Right.Color == true)
                    {
                        w.Color = false;
                        x = x.Parent;
                    }
                    else
                    {
                        if (w.Right.Color == true)
                        {
                            w.Left.Color = true;
                            w.Color = false;
                            RightRotate(w);
                            w = x.Parent.Right;
                        }
                        w.Color = x.Parent.Color;
                        x.Parent.Color = true;
                        w.Right.Color = true;
                        LeftRotate(x.Parent);
                        x = _root;
                    }
                }
                else
                {
                    Node w = x.Parent.Left;
                    if (w.Color == false)
                    {
                        w.Color = true;
                        x.Parent.Color = false;
                        RightRotate(x.Parent);
                        w = x.Parent.Left;
                    }
                    if (w.Left.Color == true && w.Right.Color == true)
                    {
                        w.Color = false;
                        x = x.Parent;
                    }
                    else
                    {
                        if (w.Left.Color == true)
                        {
                            w.Right.Color = true;
                            w.Color = false;
                            LeftRotate(w);
                            w = x.Parent.Left;
                        }
                        w.Color = x.Parent.Color;
                        x.Parent.Color = true;
                        w.Left.Color = true;
                        RightRotate(x.Parent);
                        x = _root;
                    }
                }
            }
            x.Color = true;
        }
        public Node FindNode(int z)
        {
            Node n = _root;
            while (n.Data != z && n != _nil)
            {
                if (n.Data > z)
                    n = n.Left;
                else
                    n = n.Right;
            }
            return n;
        }
        private int FindMinimum()
        {
            Node z = Minimum(_root);
            return z.Data;
        }
        private Node Minimum(Node z)
        {
            while (z.Left != _nil)
                z = z.Left;

            return z;
        }
        private void PrintHelp(Node n)
        {
            if (n != _nil)
            {
                Key p = IntToKey(n.Data);
                PrintHelp(n.Left);
                Console.WriteLine(p.Field1 + "." + p.Field2 + "." +
                    p.Field3 + " " + n.Color + " " + n.Count +
                    " Parent " + n.Parent.Data + " Left " + n.Left.Data + " Right " + n.Right.Data);
                PrintHelp(n.Right);
            }
        }
        private void LRPrintHelp(Node n)
        {
            if (n != _nil)
            {
                Key p = IntToKey(n.Data);
                LRPrintHelp(n.Left);
                Console.WriteLine(p.Field1 + "." + p.Field2 + "." +
                    p.Field3 + ", quantity: " + n.Count);
                LRPrintHelp(n.Right);
            }
        }
        private void RLPrintHelp(Node n)
        {
            if (n != _nil)
            {
                Key p = IntToKey(n.Data);
                RLPrintHelp(n.Right);
                Console.WriteLine(p.Field1 + "." + p.Field2 + "." +
                    p.Field3 + ", quantity: " + n.Count);
                RLPrintHelp(n.Left);
            }
        }
        private void NLRPrintHelp(Node n)
        {
            if (n != _nil)
            {
                Key p = IntToKey(n.Data);
                Console.WriteLine(p.Field1 + "." + p.Field2 + "." +
                    p.Field3 + ", quantity: " + n.Count);
                NLRPrintHelp(n.Left);
                NLRPrintHelp(n.Right);
            }
        }
        private void DrawHelp(Node root, int h)
        {
            string actualcolor;
            if (root != _nil)
            {
                Key p = IntToKey(root.Data);
                DrawHelp(root.Right, h + 8);
                for (int i = 0; i < h; i++)
                    Console.Write(" ");
                if (root.Color)
                    actualcolor = "black";
                else
                    actualcolor = "red";
                Console.WriteLine(p.Field1 + "." + p.Field2 + "." +
                    p.Field3 + ", count: " + root.Count + " " + actualcolor);
                DrawHelp(root.Left, h + 8);
            }
        }
    }

}
