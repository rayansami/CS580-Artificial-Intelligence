using System;
using System.Collections.Generic;

namespace _24_Puzzle_Problem_Uninformed_Search
{
    public class DfsController
    {
        private Node _root;

        public DfsController(Node root)
        {
            _root = root;
        }

        public void Search()
        {
            Stack<Node> StackList = new Stack<Node>();
            var VisitedList = new List<Node>();

            StackList.Push(_root);
            VisitedList.Add(_root);

            while (StackList.Count > 0)
            {
                Console.WriteLine(String.Format("Stack count:{0}, Visited count:{1}", StackList.Count, VisitedList.Count));
                Node currentNode = StackList.Peek();
                StackList.Pop();
                VisitedList.Add(currentNode);

                Console.WriteLine("Currently Expanding:");
                currentNode.PrintArrangement();

                if (currentNode.IsGoalFound())
                {
                    Console.WriteLine("Goal Found");
                    Node current = currentNode;
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
                        StackList.Push(child);                        
                    }
                    else
                    {
                        Console.WriteLine("---- Found a visited state! ----");
                        child.PrintArrangement();
                        Console.WriteLine("---- # ----");
                    }
                }
            }
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
