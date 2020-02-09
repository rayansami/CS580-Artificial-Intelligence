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
                                    {6,0,13,19,10},
                                    {11,21,12,1,20},
                                    {16,4,14,22,15},
                                    {8,18,23,2,7}
                                };                

                Node root = new Node(initialState,0,0); // State,level,fvalue(set later from the search)

                startTime = DateTime.Now;                
                var informedSearch = new InformedSearch(root);
                informedSearch.Search();
                endTime = DateTime.Now;

                var elapseTime = ((TimeSpan)(endTime - startTime)).TotalSeconds;
                Console.WriteLine(String.Format("Time required:{0}", elapseTime));
            }
        }
    }
}
