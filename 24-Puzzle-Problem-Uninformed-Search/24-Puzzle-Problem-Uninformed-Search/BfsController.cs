using System;
using System.Collections.Generic;

namespace _24_Puzzle_Problem_Uninformed_Search
{
    public class BfsController
    {
        private readonly Node _root;

        public BfsController(Node root)
        {
            _root = root;
        }

        public void Search()
        {
            List<Node> Queue = new List<Node>();
            List<Node> VisitedList = new List<Node>();

            Queue.Add(_root);
            VisitedList.Add(_root);

            while (Queue.Count > 0)
            {
                Console.WriteLine(String.Format("Queue count:{0},Visited count:{1}", Queue.Count, VisitedList.Count));
                //Next two line of C# list works as a Queue
                Node currentNode = Queue[0];
                Queue.RemoveAt(0);

                Console.WriteLine("Currently Expanding:");
                currentNode.PrintArrangement();

                if (currentNode.IsGoalFound())
                {
                    Console.WriteLine("Goal Found");

                    PathFinder(currentNode);
                    break;
                }

                currentNode.ExpandNode();
                //Console.WriteLine("Children arrangements are:");
                //currentNode.childNodes.ForEach(node => node.PrintArrangement());
                for (int i = 0; i < currentNode.childNodes.Count; i++)
                {
                    Node child = currentNode.childNodes[i];
                    if (!IsVisited(VisitedList, child))
                    {
                        Queue.Add(child);
                        VisitedList.Add(child);
                    }
                    else
                    {
                        Console.WriteLine("---- Found a visited state! ----");
                        child.PrintArrangement();
                        Console.WriteLine("---- # ----");
                    }
                }
            }

            if(Queue.Count == 0)
                Console.WriteLine("Goal wasn not found!");
        }

        private bool IsVisited(List<Node> visitedList, Node nodeToCheck)
        {
            var contains = false;
            foreach (var node in visitedList)
            {
                if (node.IsSameArrangement(nodeToCheck.arrangement))
                {
                    contains = true;
                }
            }

            return contains;
        }

        private void PathFinder(Node currentNode)
        {
            int moves = 0;
            while (currentNode != null)
            {
                currentNode.PrintArrangement();
                currentNode = currentNode.parentNode;
                moves++;
            }
            Console.WriteLine(String.Format("Move required: {0}", (moves - 1)));
        }
    }
}
