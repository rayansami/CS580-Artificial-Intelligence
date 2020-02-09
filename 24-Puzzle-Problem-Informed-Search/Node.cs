using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _24_Puzzle_Problem_Informed_Search
{
    public class Node
    {
        private readonly int ROW;// = 3;
        private readonly int COL;// = 3;        
        //const int ARRAY_SIZE = 9;
        private int[,] goalArrangement = new int[,]
                        {
                                        {1,2,3},
                                        {8,0,4},
                                        {7,6,5}
                        };

        public int[,] arrangement;
        public List<Node> childNodes = new List<Node>();
        public Node parentNode;


        public Node(int[,] arr)
        {
            this.arrangement = arr.Clone() as int[,];
            ROW = arrangement.GetLength(0);
            COL = arrangement.GetLength(1);
        }

        public bool IsGoalFound()
        {
            var goalFound = true;
            //int[,] goalArrangement = new int[,]
            //                        {
            //                            {1,2,3},
            //                            {8,0,4},
            //                            {7,6,5}
            //                        };

            if (!IsSameArrangement(goalArrangement))
                goalFound = false;


            return goalFound;
        }

        public bool IsSameArrangement(int[,] arrangementToCheck)
        {
            return arrangement.Rank == arrangementToCheck.Rank && Enumerable.Range(0, arrangement.Rank).All(dimension => arrangement.GetLength(dimension) == arrangementToCheck.GetLength(dimension)) && arrangement.Cast<int>().SequenceEqual(arrangementToCheck.Cast<int>());
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
                //SwapPositionAndCreateNewChild(possibleArrangement, zeroX + 1, zeroX, zeroY);
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
            Node child = new Node(possibleArrangement);
            childNodes.Add(child);
            child.parentNode = this;
        }

        public void PrintArrangement()
        {
            //int idx = 0;
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

        // Checking the misplaced positions
        public int Huristics()
        {
            int number = 0;
            var manhattanDistance = 0;
            for (int i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COL; j++)
                {
                    number = arrangement[i, j];
                    //if (arrangement[i, j] != goalArrangement[i, j])
                    //{
                    //    totalMisplacedNumbers++;    
                    //};
                    if (number == 1)
                        manhattanDistance += sumDistance(i, j, 0, 0);// Math.Abs(i - 2) + Math.Abs(j-2);
                    if (number == 2)
                        manhattanDistance += sumDistance(i, j, 0, 1);
                    if (number == 3)
                        manhattanDistance += sumDistance(i, j, 0, 2);
                    if (number == 4)
                        manhattanDistance += sumDistance(i, j, 1, 2);
                    if (number == 5)
                        manhattanDistance += sumDistance(i, j, 2, 2);
                    if (number == 6)
                        manhattanDistance += sumDistance(i, j, 2, 1);
                    if (number == 7)
                        manhattanDistance += sumDistance(i, j, 2, 0);
                    if (number == 8)
                        manhattanDistance += sumDistance(i, j, 1, 0);
                }
            }

            return manhattanDistance;
        }

        private int sumDistance(int i, int j, int idealPosX, int idealPosY)
        {
            return (Math.Abs(i - idealPosX) + Math.Abs(j - idealPosY));
        }
    }
}
