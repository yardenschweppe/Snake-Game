using System;

public class Circle
{


        public int X { get; set; }
    public int Y { get; set; }
    public Brush Brush { get; set; }


        public Circle(int x, int y, Brush brush)
        {
            X = x;
            Y = y;
            Brush = brush;
        }

    public Circle()
    {
    }
}