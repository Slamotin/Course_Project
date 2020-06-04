using System;

namespace WpfApp1
{
    class Node
    {
        public readonly int Key;
        public int Height;
        public Node Left, Right;

        public Node(int key)
        {
            Key = key;
            Height = 1;
        }
    }

    class Bst
    {
        private Node root;

        private int GetHeight(Node node) => node?.Height ?? 0;

        private int GetBalance(Node node) => node == null ? 0 : GetHeight(node.Left) - GetHeight(node.Right);

        private Node RightRotate(Node y)
        {
            Node x = y.Left;
            Node T2 = x.Right;

            x.Right = y;
            y.Left = T2;
            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;

            return x;
        }

        private Node LeftRotate(Node x)
        {
            Node y = x.Right;
            Node T2 = y.Left;

            y.Left = x;
            x.Right = T2;
            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;

            return y;
        }

        public void Insert(int key) => root = Insert(root, key);

        private Node Insert(Node node, int key)
        {
            if (node == null)
                return new Node(key);

            if (key < node.Key)
                node.Left = Insert(node.Left, key);
            else if (key > node.Key)
                node.Right = Insert(node.Right, key);
            else
                return node;

            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
            int balance = GetBalance(node);
            if (balance > 1 && key < node.Left.Key)
                return RightRotate(node);
            if (balance < -1 && key > node.Right.Key)
                return LeftRotate(node);
            if (balance > 1 && key > node.Left.Key)
            {
                node.Left = LeftRotate(node.Left);
                return RightRotate(node);
            }

            if (balance < -1 && key < node.Right.Key)
            {
                node.Right = RightRotate(node.Right);
                return LeftRotate(node);
            }

            return node;
        }

        public bool Contains(int key) => Contains(root, key);

        private bool Contains(Node node, int key)
        {
            if (node == null)
                return false;
            if (node.Key > key)
                return Contains(node.Left, key);
            if (node.Key < key)
                return Contains(node.Right, key);
            return true;
        }
    }
}