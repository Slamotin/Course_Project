using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WpfApp1
{ 
    class Node<T>
    {
        public T Value { get; set; }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }

        public Node(T value)
        {
            Value = value;
        }
    }
    
    public class BST<T> where T : IComparable
    {
        private Node<T> root;

        public void Add(T key) => Add(key, root);

        private void Add(T key, Node<T> node)
        {
            if (key == null)
                throw new ArgumentException("key is null");
            if (root == null)
            {
                root = new Node<T>(key);
                return;
            }
            if (key.CompareTo(node.Value) < 0)
                if (node.Left == null)
                    node.Left = new Node<T>(key);
                else
                    Add(key, node.Left);
            if (key.CompareTo(node.Value) > 0)
                if (node.Right == null)
                    node.Right = new Node<T>(key);
                else
                    Add(key, node.Right);
        }

        public bool Contains(T key) => Contains(key, root);
        
        private bool Contains(T key, Node<T> node)
        {
            if (node == null)
                return false;
            if (key.Equals(node.Value))
                return true;
            if (key.CompareTo(node.Value) < 0)
                return Contains(key, node.Left);
            return Contains(key, node.Right);
        }
    }
}