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
                                { {5,6,7},{4,0,8},{3,2,1}
                                };
                /*
                   easy: 1,3,4,8,6,2,7,0,5
                 * medium: 2,8,1,0,4,3,7,6,5
                 * hard: 2,8,1,4,6,3,0,7,5
                 * worst: 5,6,7,4,0,8,3,2,1
                 */


                Node root = new Node(initialState);



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
