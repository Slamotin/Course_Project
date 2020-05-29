using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WpfApp1
{ 
    class Node<T>
    {
        public T value { get; set; }
        public Node<T> left { get; set; }
        public Node<T> right { get; set; }

        public Node(T value)
        {
            this.value = value;
        }
    }
    
    public class BinaryTree<T> where T : IComparable
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
            if (key.CompareTo(node.value) < 0)
                if (node.left == null)
                    node.left = new Node<T>(key);
                else
                    Add(key, node.left);
            if (key.CompareTo(node.value) > 0)
                if (node.right == null)
                    node.right = new Node<T>(key);
                else
                    Add(key, node.right);
        }

        public bool Contains(T key) => Contains(key, root);
        
        private bool Contains(T key, Node<T> node)
        {
            if (node == null)
                return false;
            if (key.Equals(node.value))
                return true;
            if (key.CompareTo(node.value) < 0)
                return Contains(key, node.left);
            return Contains(key, node.right);
        }
    }
}