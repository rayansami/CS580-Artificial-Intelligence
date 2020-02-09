using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _24_Puzzle_Problem_Informed_Search
{
    public class Node
    {
        private readonly int ROW;// = 5;
        private readonly int COL;// = 5;    
        public int fvalue; //sum of the cost to reach THIS node and the heuristic value of THIS node
        public int level; // the number of moves so far
        
        private int[,] goalArrangement = new int[,]
                        {   {1,2,3,4,5 },
                            { 6,7,8,9,10},
                            { 11,12,13,14,15},
                            { 16,17,18,19,20},
                            { 21,22,23,24,0}
                        };


        public int[,] arrangement;
        public List<Node> childNodes = new List<Node>();
        public Node parentNode;


        public Node(int[,] arr,int level,int fvalue)
        {
            this.arrangement = arr.Clone() as int[,];
            ROW = arrangement.GetLength(0);
            COL = arrangement.GetLength(1);

            this.level = level;
            this.fvalue = fvalue;
        }

        public bool IsGoalFound()
        {
            var goalFound = true;

            if (!IsSameArrangement(goalArrangement))
                goalFound = false;


            return goalFound;
        }

        public bool IsSameArrangement(int[,] arrangementToCheck)
        {                        
            for (var i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COL; j++)
                {
                    if (arrangement[i, j] != arrangementToCheck[i, j])
                    {                     
                        return false;
                    }
                }
            }
            return true;
        }

        public void ExpandNode()
        {
            for (var i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COL; j++)
                {
                    if (arrangement[i, j] == 0)
                    {
                        int xIndexOfzero = i;
                        int yIndexOfzero = j;
                        MoveZeroToUp(arrangement, xIndexOfzero, yIndexOfzero);
                        MoveZeroToDown(arrangement, xIndexOfzero, yIndexOfzero);
                        MoveZeroToLeft(arrangement, xIndexOfzero, yIndexOfzero);
                        MoveZeroToRight(arrangement, xIndexOfzero, yIndexOfzero);
                    }
                }
            }
        }

        public void MoveZeroToRight(int[,] arr, int zeroX, int zeroY)
        {
            if (zeroX + 1 < COL)
            {
                int[,] possibleArrangement = arr.Clone() as int[,];
                
                var zero = possibleArrangement[zeroX, zeroY];
                possibleArrangement[zeroX, zeroY] = possibleArrangement[zeroX + 1, zeroY];
                possibleArrangement[zeroX + 1, zeroY] = zero;

                createNewChild(possibleArrangement);
            }
        }

        public void MoveZeroToLeft(int[,] arr, int zeroX, int zeroY)
        {
            if (zeroX - 1 >= 0)
            {
                int[,] possibleArrangement = arr.Clone() as int[,];

                var zero = possibleArrangement[zeroX, zeroY];
                possibleArrangement[zeroX, zeroY] = possibleArrangement[zeroX - 1, zeroY];
                possibleArrangement[zeroX - 1, zeroY] = zero;

                createNewChild(possibleArrangement);
            }
        }
        public void MoveZeroToUp(int[,] arr, int zeroX, int zeroY)
        {
            if (zeroY - 1 >= 0)
            {
                int[,] possibleArrangement = arr.Clone() as int[,];

                var zero = possibleArrangement[zeroX, zeroY];
                possibleArrangement[zeroX, zeroY] = possibleArrangement[zeroX, zeroY - 1];
                possibleArrangement[zeroX, zeroY - 1] = zero;

                createNewChild(possibleArrangement);
            }
        }
        public void MoveZeroToDown(int[,] arr, int zeroX, int zeroY)
        {
            if (zeroY + 1 < ROW)
            {
                int[,] possibleArrangement = arr.Clone() as int[,];

                var zero = possibleArrangement[zeroX, zeroY];
                possibleArrangement[zeroX, zeroY] = possibleArrangement[zeroX, zeroY + 1];
                possibleArrangement[zeroX, zeroY + 1] = zero;

                createNewChild(possibleArrangement);
            }
        }
        private void createNewChild(int[,] possibleArrangement)
        {
            Node child = new Node(possibleArrangement,level+1,0); /* level is always +1 than its parent.
                                                                     Setting fvalue = 0 initially,will calculate it later on the search process
                                                                   */
            childNodes.Add(child);
            child.parentNode = this;
        }

        public void PrintArrangement()
        {
        
            for (int i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COL; j++)
                {
                    Console.Write(arrangement[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        

        // Calculating Manhattan distance
        public int Huristics()
        {                        
            int manhattanDistance = 0;            
            for (int i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COL; j++)
                {

                    int number = arrangement[i, j];
                    if (number == 1)
                        manhattanDistance += sumDistance(i, j, 0, 0);// Math.Abs(i - 2) + Math.Abs(j-2);
                    if (number == 2)
                        manhattanDistance += sumDistance(i, j, 0, 1);
                    if (number == 3)
                        manhattanDistance += sumDistance(i, j, 0, 2);
                    if (number == 4)
                        manhattanDistance += sumDistance(i, j, 0, 3);
                    if (number == 5)
                        manhattanDistance += sumDistance(i, j, 0, 4);
                    if (number == 6)
                        manhattanDistance += sumDistance(i, j, 1, 0);
                    if (number == 7)
                        manhattanDistance += sumDistance(i, j, 1, 1);
                    if (number == 8)
                        manhattanDistance += sumDistance(i, j, 1, 2);
                    if (number == 9)
                        manhattanDistance += sumDistance(i, j, 1, 3);
                    if (number == 10)
                        manhattanDistance += sumDistance(i, j, 1, 4);
                    if (number == 11)
                        manhattanDistance += sumDistance(i, j, 2, 0);
                    if (number == 12)
                        manhattanDistance += sumDistance(i, j, 2, 1);
                    if (number == 13)
                        manhattanDistance += sumDistance(i, j, 2, 2);
                    if (number == 14)
                        manhattanDistance += sumDistance(i, j, 2, 3);
                    if (number == 15)
                        manhattanDistance += sumDistance(i, j, 2, 4);
                    if (number == 16)
                        manhattanDistance += sumDistance(i, j, 3, 0);
                    if (number == 17)
                        manhattanDistance += sumDistance(i, j, 3, 1);
                    if (number == 18)
                        manhattanDistance += sumDistance(i, j, 3, 2);
                    if (number == 19)
                        manhattanDistance += sumDistance(i, j, 3, 3);
                    if (number == 20)
                        manhattanDistance += sumDistance(i, j, 3, 4);
                    if (number == 21)
                        manhattanDistance += sumDistance(i, j, 4, 0);
                    if (number == 22)
                        manhattanDistance += sumDistance(i, j, 4, 1);
                    if (number == 23)
                        manhattanDistance += sumDistance(i, j, 4, 2);
                    if (number == 24)
                        manhattanDistance += sumDistance(i, j, 4, 3);
                }
            }

            //return totalMisplacedNumbers;
            return manhattanDistance;
        }      

        private int sumDistance(int i, int j, int idealPosX, int idealPosY)
        {
            return (Math.Abs(i - idealPosX) + Math.Abs(j - idealPosY));
        }
      
    }
}


/*
 * 
 *          totalMisplacedNumbers = 0;
            for (int i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COL; j++)
                {
                    
                    if (arrangement[i, j] != goalArrangement[i, j])
                    {
                        totalMisplacedNumbers++;
                    };
                }       
            }
     */
