using System;
using System.Collections.Generic;
using System.Text;

namespace _24_Puzzle_Problem_Informed_Search
{
    // Got it from here and edited it into my purpose (http://csharphelper.com/blog/2015/02/make-a-generic-priority-queue-class-in-c/)
    class PriorityQueue<T>
    {
        // The items and priorities.
        List<T> Values = new List<T>();
        List<int> Priorities = new List<int>();        

        // Return the number of items in the queue.
        public int Count
        {
            get
            {
                return Values.Count;
            }
        }

        // Add an item to the queue.
        public void Enqueue(T new_value, int new_priority)
        {
            Values.Add(new_value);
            Priorities.Add(new_priority);
        }
        

        // Remove the item with the lowest priority from the queue.
        public void Dequeue()
        {
            // Find the lowest priority.            
            int best_index = 0;
            int best_priority = Priorities[0];
            for (int i = 1; i < Priorities.Count; i++)
            {
                if (best_priority > Priorities[i])
                {
                    best_priority = Priorities[i];
                    best_index = i;
                }
            }

            // Return the corresponding item.
            // top_value = Values[best_index];
            // top_priority = best_priority;
            
            // Remove the item from the lists.
            Values.RemoveAt(best_index);
            Priorities.RemoveAt(best_index);           
        }

        public T First()
        {
            // Find the lowest priority.            
            int best_index = 0;
            int best_priority = Priorities[0];
            for (int i = 1; i < Priorities.Count; i++)
            {
                if (best_priority > Priorities[i])
                {
                    best_priority = Priorities[i];
                    best_index = i;
                }
            }

           // ValueAndPriyority(out best_index,out best_priority);
            return Values[best_index];            
        }
    }
}
