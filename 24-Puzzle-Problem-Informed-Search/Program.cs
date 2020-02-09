using System;

namespace _24_Puzzle_Problem_Informed_Search
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                DateTime startTime, endTime;
                int[,] initialState = new int[,]
                                {   {9,24,3,5,17},
                                    {6,1,13,19,10},
                                    {11,21,22,0,20},
                                    {16,4,14,12,15},
                                    {8,18,23,2,7}
                                };
                /*
                   easy: 1,3,4,8,6,2,7,0,5
                 * medium: 2,8,1,0,4,3,7,6,5
                 * hard: 2,8,1,4,6,3,0,7,5
                 * worst: {5,6,7},{4,0,8},{3,2,1}
                 */



                Node root = new Node(initialState,0,0);



                startTime = DateTime.Now;

                //Informed Search
                var informedSearch = new InformedSearch(root);
                informedSearch.Search();

                endTime = DateTime.Now;
                var elapseTime = ((TimeSpan)(endTime - startTime)).TotalSeconds;
                Console.WriteLine(String.Format("Time required:{0}", elapseTime));
            }
        }
    }
}
