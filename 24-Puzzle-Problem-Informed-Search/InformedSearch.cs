using C5;
using System;
using System.Collections.Generic;
using System.Text;

namespace _24_Puzzle_Problem_Informed_Search
{
    public class InformedSearch
    {
        private Node _root;
        private int nodeDistance;
        private int distance = int.MaxValue;
        public InformedSearch(Node root)
        {
            _root = root;
            nodeDistance = _root.Huristics();
        }

        public void Search()
        {
            var VisitedList = new List<Node>();
            var pq = new PriorityQueue<Node>();

            int rootCost = _root.Huristics();
            pq.Enqueue(_root, rootCost);

            while (pq.Count > 0)
            {
                Console.WriteLine(String.Format("Queue count:{0}", pq.Count));
                Node current = pq.First();
                //int currentCost = current.ManhattanDistance();
                VisitedList.Add(current);
                pq.Dequeue();

                Console.WriteLine("Currently Expanding:");
                current.PrintArrangement();

                if (current.IsGoalFound())
                {
                    Console.WriteLine("Goal Found");
                    Node currentNode = current;
                    PathFinder(currentNode);
                    break;
                }

                Console.WriteLine("Children arrangements are:");
                current.ExpandNode();
                for (int i = 0; i < current.childNodes.Count; i++)
                {
                    var childNode = current.childNodes[i];
                    childNode.PrintArrangement();
                    var distanceToRoot = childNode.Huristics();
                    Console.WriteLine("This arrangement has a cost of:" + distanceToRoot);
                }

                for (int i = 0; i < current.childNodes.Count; i++)
                {
                    var childNode = current.childNodes[i];
                    if (!IsVisited(VisitedList, childNode))
                    {
                        pq.Enqueue(childNode, childNode.Huristics());
                        VisitedList.Add(childNode);
                    }
                    else
                    {
                        Console.WriteLine("---- Found a visited state! ----");
                        childNode.PrintArrangement();
                        Console.WriteLine("---- # ----");
                    }

                }
            }
        }

        private bool IsVisited(List<Node> visitedList, Node childNode)
        {
            var contains = false;
            foreach (var node in visitedList)
            {
                if (node.IsSameArrangement(childNode.arrangement))
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
