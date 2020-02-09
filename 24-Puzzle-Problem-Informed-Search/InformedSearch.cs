using C5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _24_Puzzle_Problem_Informed_Search
{
    public class InformedSearch
    {
        private Node _root;
                
        public InformedSearch(Node root)
        {
            _root = root;       
        }

        public void Search()
        {
            var VisitedList = new List<Node>(); 
            var pq = new List<Node>(); // Works as Priyority Queue

            _root.fvalue = _root.Huristics() + _root.level; // Setting up fvalue for root node
            pq.Add(_root);

            while (pq.Count > 0)
            {
                Console.WriteLine(String.Format("Queue count:{0}", pq.Count));
                Node current = pq[0];
                VisitedList.Add(current);

                Console.WriteLine("Currently Expanding:");
                current.PrintArrangement();

                Console.WriteLine(String.Format("Current huristics:{0}", current.Huristics()));
                if (current.Huristics() == 0)
                {
                    Console.WriteLine("Goal Found");
                    Node currentNode = current;
                    PathFinder(currentNode);
                    break;
                }
                
                current.ExpandNode();                                

                for (int i = 0; i < current.childNodes.Count; i++)
                {
                    var childNode = current.childNodes[i];
                    if(IsContained(VisitedList, childNode)) // If childNode is visited once then do nothing and go to next childNode
                    {
                        continue;
                    }

                    if (!IsContained(pq,childNode)) // (If not visited already)Check if chilNode exists in the priyority queue list already. If not, add to pq
                    {
                        childNode.fvalue = childNode.Huristics() + childNode.level; //Calculate fvalue for the childNode
                        pq.Add(childNode);
                    }
                    else
                    {
                        // If already existing node has better level (cost to traverse till This node from root), then swap the currentNode with that 
                        var alreadyExistingNodeIndex = IndexOfExistingNodeInPriyorityQueue(pq,childNode);
                        Node alreadyExistingNode = pq[alreadyExistingNodeIndex];

                        if(alreadyExistingNode.level < childNode.level)
                        {
                            alreadyExistingNode.level = childNode.level;
                            alreadyExistingNode.fvalue = childNode.fvalue;
                            alreadyExistingNode.parentNode = childNode.parentNode;
                        }
                    }
                }

                pq.RemoveAt(0); // Removing the node that is already traversed with lowest fvalue                    
                pq = pq.OrderBy(node => node.fvalue).ToList(); //Order the list based of fvalue
            }            
        }

        // To check if a node exist in passed list
        private bool IsContained(List<Node> nodeList, Node childNode)
        {
            var contains = false;
            foreach (var node in nodeList)
            {
                if (node.IsSameArrangement(childNode.arrangement))
                {
                    contains = true;
                }
            }

            return contains;
        }

        
        private int IndexOfExistingNodeInPriyorityQueue(List<Node> queueList, Node node)
        {
            int index = 0;
            for(int i=0; i < queueList.Count; i++)
            {
                if (node.IsSameArrangement(queueList[i].arrangement))
                {
                    index = i;
                }
            }
            return index;
        }

        private void PathFinder(Node currentNode)
        {
            int moves = 0;            
            Console.WriteLine(String.Format("Level Traversed: {0}", currentNode.level));
            while (currentNode != null)
            {
                currentNode.PrintArrangement();
                currentNode = currentNode.parentNode;
                moves++;
            }
          //  Console.WriteLine(String.Format("Move required: {0}", (moves - 1)));
        }
    }
}
