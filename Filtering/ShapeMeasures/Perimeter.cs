using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ImageProcessing.Filtering.ShapeMeasures
{
    public class Perimeter
    {
        
        /// <summary>
        /// Calculates the perimeter, meaning the distance around the object
        /// </summary>
        public Perimeter()
        {

        }
        public enum Direction
        {
            E, N, S, W

            //E(1, 0), NE(1, 1),

            //N(0, 1), NW(-1, 1),

            //W(-1, 0), SW(-1, -1),

            //S(0, -1), SE(1, -1);
        }


        public static float Calculate(Image image)
        {
            for (int x = 0; x < image.Size.Width; x++)
            {
                for (int y = 0; y < image.Size.Height; y++)
                {
                    if (image.GetPixelColor(x, y) == Image.Black)
                    {
                        return Calculate(x, y, image);
                    }
                }
            }
            return 0;
        }

        public static float Calculate(int initialX, int initialY, Image image)
        {
            //todo: route bepalen
            int initialValue = value(initialX, initialY, image);
            if (initialValue == 0 || initialValue == 15)
                throw new Exception(String.Format("Supplied initial coordinates (%d, %d) do not lie on a perimeter.", initialX, initialY));

            List<Direction> directions = new List<Direction>();

            int x = initialX;
            int y = initialY;
            Direction? previous = null;

            do
            {
                Direction direction;
                switch (value(x, y, image))
                {
                    case 1: direction = Direction.N; break;
                    case 2: direction = Direction.E; break;
                    case 3: direction = Direction.E; break;
                    case 4: direction = Direction.W; break;
                    case 5: direction = Direction.N; break;
                    case 6: direction = previous == Direction.N ? Direction.W : Direction.E; break;
                    case 7: direction = Direction.E; break;
                    case 8: direction = Direction.S; break;
                    case 9: direction = previous == Direction.E ? Direction.N : Direction.S; break;
                    case 10: direction = Direction.S; break;
                    case 11: direction = Direction.S; break;
                    case 12: direction = Direction.W; break;
                    case 13: direction = Direction.N; break;
                    case 14: direction = Direction.W; break;
                    default: throw new Exception("Wrong directional input!");
                }
                directions.Add(direction);
                SetCoordinateValuesBasedOnDirection(ref x, ref y, direction);
                previous = direction;
            } while (x != initialX || y != initialY);

            return CountPath(directions);
        }

        private static float CountPath(List<Direction> directions)
        {
            //float total = 0;
            //foreach (Direction d in directions)
            //{

            //    switch (d)
            //    {
            //        case N: total++; break;
            //        case 2: direction = E; break;
            //        case 3: direction = E; break;
            //        case 4: direction = W; break;
            //    }
            //}
            return directions.Count;
        }

        private static int value(int x, int y, Image image)
        {
            int sum = 0;
            if (isSet(x, y, image)) sum |= 1;
            if (isSet(x + 1, y, image)) sum |= 2;
            if (isSet(x, y + 1, image)) sum |= 4;
            if (isSet(x + 1, y + 1, image)) sum |= 8;
            return sum;
        }

        private static bool isSet(int x, int y, Image image)
        {
            return x <= 0 || x > image.Size.Width || y <= 0 || y > image.Size.Height ?
                false :
                image.GetPixelColor(x-1, y-1) != Image.Black;
                //data[(y - 1) * width + (x - 1)] != 0;
        }

        private static void SetCoordinateValuesBasedOnDirection(ref int x, ref int y, Direction direction)
        {

            switch (direction)
            {
                case Direction.E: { x += 1; y -= 0; }; break;
                case Direction.N: { x += 0; y -= 1; }; break;
                case Direction.W: { x += -1; y -= 0; }; break;
                case Direction.S: { x += 0; y -= -1; }; break;
                    //E(1, 0), NE(1, 1),

                    //N(0, 1), NW(-1, 1),

                    //W(-1, 0), SW(-1, -1),

                    //S(0, -1), SE(1, -1);
            }
        }
    }
}
