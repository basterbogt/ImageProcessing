using System.Drawing;

namespace ImageProcessing.Filtering.ShapeMeasures
{
    /// <summary>
    /// Chord class, which holds the values of a chord
    /// </summary>
    public class Chord
    {
        public double Length { get; private set; }
        public double Rotation { get; private set; }
        public Point StartingPoint { get; private set; }
        public Point EndingPoint { get; private set; }
        
        public Chord(double length, double rotation, Point startingPoint, Point endingPoint)
        {
            this.Length = length;
            this.Rotation = rotation;
            this.StartingPoint = startingPoint;
            this.EndingPoint = endingPoint;
        }


    }
}
