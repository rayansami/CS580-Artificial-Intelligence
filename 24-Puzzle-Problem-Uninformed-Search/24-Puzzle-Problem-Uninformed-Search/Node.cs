using System;
using System.Collections.Generic;
using System.Linq;

namespace _24_Puzzle_Problem_Uninformed_Search
{
    public class Node
    {

        const int ROW = 5; 
        const int COL = 5;

        public int[] arrangement;
        public List<Node> childNodes = new List<Node>();
        public Node parentNode;


        public Node(int[] arr)
        {
            this.arrangement = arr.Clone() as int[];            
        }

        public bool IsGoalFound()
        {
            var goalFound = true;
            int[] goalArrangement = new int[]
                                    { 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,0};

            if (!IsSameArrangement(goalArrangement))
                goalFound = false;


            return goalFound;
        }

        public bool IsSameArrangement(int[] arrangementToCheck)
        {
            for(int i=0; i< arrangement.Length; i++)
            {
                if (arrangementToCheck[i] != arrangement[i])
                    return false;
            }
            return true;
        }

        public void ExpandNode()
        {
            for (var i = 0; i < arrangement.Length; i++)
            {
                if (arrangement[i] == 0)
                {
                    int indexOfzero = i;
                    MoveZeroToUp(arrangement, indexOfzero);
                    MoveZeroToDown(arrangement, indexOfzero);
                    MoveZeroToLeft(arrangement, indexOfzero);
                    MoveZeroToRight(arrangement, indexOfzero);
                }
            }
        }

        public void MoveZeroToRight(int[] arr, int zeroPosition)
        {
            if (zeroPosition % COL < COL - 1)
            {
                int[] possibleArrangement = arr.Clone() as int[];
                SwapPositionAndCreateNewChild(possibleArrangement, zeroPosition + 1, zeroPosition);
            }
        }

        public void MoveZeroToLeft(int[] arr, int zeroPosition)
        {
            if (zeroPosition % COL > 0)
            {
                int[] possibleArrangement = arr.Clone() as int[];
                SwapPositionAndCreateNewChild(possibleArrangement, zeroPosition - 1, zeroPosition);
            }
        }
        public void MoveZeroToUp(int[] arr, int zeroPosition)
        {
            if (zeroPosition - ROW >= 0)
            {
                int[] possibleArrangement = arr.Clone() as int[];
                SwapPositionAndCreateNewChild(possibleArrangement, zeroPosition - ROW, zeroPosition);
            }
        }
        public void MoveZeroToDown(int[] arr, int zeroPosition)
        {
            if (zeroPosition < arrangement.Length - ROW)
            {
                int[] possibleArrangement = arr.Clone() as int[];
                SwapPositionAndCreateNewChild(possibleArrangement, zeroPosition + ROW, zeroPosition);
            }
        }

        private void SwapPositionAndCreateNewChild(int[] possibleArrangement, int newPosition, int oldPosition)
        {
            int temp = possibleArrangement[newPosition];
            possibleArrangement[newPosition] = possibleArrangement[oldPosition];
            possibleArrangement[oldPosition] = temp;

            Node child = new Node(possibleArrangement);
            childNodes.Add(child);
            child.parentNode = this;
        }

        public void PrintArrangement()
        {
            int idx = 0;
            for (int i = 0; i < arrangement.Length; i++)
            {
                if (idx % ROW == 0)
                    Console.WriteLine();
                Console.Write(arrangement[i] + " ");
                idx++;
            }
            Console.WriteLine();
        }
    }

}
