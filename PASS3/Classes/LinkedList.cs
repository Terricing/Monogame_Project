// Author: Eilay Katsnelson
// File Name: LinkedList.cs
// Project Name: PASS3
// Creation Date: January 6, 2022
// Modified Date: January 21, 2022
// Description: A circularly linked list where the head and the tail are linked togehter
// This allows adding new items at an O(1) time complexity. It can be used as a queue or a stack

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASS3.Classes
{
    class LinkedList
    {
        // Store head and tail of list
        private Node head;
        private Node tail;

        // Store count of list
        int count;

        /// <summary>
        /// Create empty LinkedList
        /// </summary>
        public LinkedList()
        {
            count = 0;
        }

        /// <summary>
        /// Add new items to the end of the queue
        /// </summary>
        /// <param name="obs">obstacle to enqueue</param>
        public void Enqueue(Obstacle obs)
        {
            // if count is 1, add new item after the head, otherwise add it after the current tail
            if (count == 0)
            {
                head = new Node(obs);
                tail = head;
            }
            else if (count == 1)
            {
                head.Next = new Node(obs);
                tail = head.Next;
            }
            else
            {
                tail.Next = new Node(obs);
                tail = tail.Next;
            }
            // increase count
            count++;
        }

        /// <summary>
        /// Remove items from the start of the queue, then return them
        /// </summary>
        /// <returns>Obstacle that was removed from the queue</returns>
        public Obstacle Dequeue()
        {
            // Reassign the head. Leave it to be cleaned up by the garbage collector 
            Obstacle temp = head.Val;
            head = head.Next;

            // decrease count
            count--;

            // return removed obstacle
            return temp;
        }

        /// <summary>
        /// Accessor for the head of the list
        /// </summary>
        public Node Head
        {
            get { return head; }
        }

        /// <summary>
        /// Accessor for the tail of the list
        /// </summary>
        public Node Tail
        {
            get { return tail; }
        }

        /// <summary>
        /// Property for the count of the list
        /// </summary>
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
    }
 

    // Node class to function with the linkedlist
    class Node
    {
        // Store value of current node
        private Obstacle value;

        // Store address of next node
        private Node next;

        /// <summary>
        /// Create node object
        /// </summary>
        /// <param name="value">the obstacle that is being contained</param>
        public Node(Obstacle value)
        {
            this.value = value;
        }

        /// <summary>
        /// Value property, obtain value
        /// </summary>
        public Obstacle Val
        {
            get { return value; }
        }

        /// <summary>
        /// Next node property, obtain next node
        /// </summary>
        public Node Next
        {
            get { return next; }
            set { next = value; }
        }
    }
}
