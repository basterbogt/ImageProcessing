using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows;

namespace ImageProcessing.Filtering.ShapeMeasures
{
    public class GrahamScan
    {

        public static LinkedList<System.Drawing.Point> Scan(List<System.Drawing.Point> points){

            System.Drawing.Point[] list = points.ToArray();
            QuickSort_Recursive(list, 0, points.Count -1);

            LinkedList<System.Drawing.Point> newPoints = new LinkedList<System.Drawing.Point>();
            newPoints.AddLast(list[0]);
            newPoints.AddLast(list[1]);
            int h = 3;
            for(int i = 3; i < points.Count; i++)
            {
                newPoints.AddLast(list[i]);
                h++;

                while(h > i && cww(newPoints.Last.Previous.Previous, newPoints.Last.Previous, newPoints.Last))
                {
                    newPoints.Remove(newPoints.Last.Previous);
                    h--;
                }

            }


            return newPoints;
        }

        private static bool cww(LinkedListNode<System.Drawing.Point> previous1, LinkedListNode<System.Drawing.Point> previous2, LinkedListNode<System.Drawing.Point> last)
        {
            Vector v = new Vector(previous2.Value.X - previous1.Value.X, previous2.Value.Y - previous1.Value.Y);
            Vector w = new Vector(last.Value.X - previous2.Value.X, last.Value.Y - previous2.Value.Y);
            double c = v.X * w.Y - v.Y * w.X;
            return c <= 0;
        }

        static public int Partition(System.Drawing.Point[] numbers, int left, int right)
        {
            int pivot = numbers[left].X;
            while (true)
            {
                while (numbers[left].X < pivot)
                    left++;

                while (numbers[right].X > pivot)
                    right--;

                if (left < right)
                {
                    System.Drawing.Point temp = numbers[right];
                    numbers[right] = numbers[left];
                    numbers[left] = temp;
                }
                else
                {
                    return right;
                }
            }
        }
        static public void QuickSort_Recursive(System.Drawing.Point[] numbers, int left, int right)
        {
            // For Recusrion
            if (left < right)
            {
                int pivot = Partition(numbers, left, right);

                if (pivot > 1)
                    QuickSort_Recursive(numbers, left, pivot - 1);

                if (pivot + 1 < right)
                    QuickSort_Recursive(numbers, pivot + 1, right);
            }
        }
    }
}
