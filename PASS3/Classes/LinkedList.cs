// create a circularly linked list where the head and the tail link together
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

        // Create empty LinkedList
        public LinkedList()
        {
            count = 0;
        }

        // Add new items to the end of queue
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

        // Remove items from the start of the queue, then return them
        public Obstacle Dequeue()
        {
            // Reassign the head. Leave it to be cleaned up by the garbage collector 
            Obstacle temp = head.Val;
            head = head.Next;
            count--;
            return temp;
        }

        public Node Head
        {
            get { return head; }
        }

        public Node Tail
        {
            get { return tail; }
        }

        // Count property, obtain count
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

        // Create node object
        public Node(Obstacle value)
        {
            this.value = value;
        }

        // Value property, obtain value
        public Obstacle Val
        {
            get { return value; }
        }

        // Next node property, obtain next node
        public Node Next
        {
            get { return next; }
            set { next = value; }
        }
    }
}
