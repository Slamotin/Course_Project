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
    
    public class BST<T> : IEnumerable<T> where T : IComparable
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
            return key.Equals(node.Value) || Contains(key, key.CompareTo(node.Value) < 0 ? node.Left : node.Right);
        }
        
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<T> GetEnumerator()
        {
            var stack = new Stack<Node<T>>();
            var node = root;
            while (node != null || stack.Count > 0)
            {
                while (node != null)
                {
                    stack.Push(node);
                    node = node.Left;
                }
                node = stack.Pop();
                yield return node.Value;
                node = node.Right;
            }
        }
        
        public T this[int index] // O(n), be careful boi
        {
            get => this.Skip(index).FirstOrDefault();
        }
    }
}