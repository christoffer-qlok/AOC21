using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC21
{
    internal class Coord
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coord(int y, int x)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object? obj)
        {
            return obj is Coord coord &&
                   X == coord.X &&
                   Y == coord.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static Coord operator +(Coord c1, Coord c2)
        {
            return new Coord(c1.Y + c2.Y, c1.X + c2.X);
        }

        public static Coord operator *(int i, Coord c)
        {
            return new Coord(c.Y * i, c.X * i);
        }

        public static Coord operator *(Coord c, int i)
        {
            return new Coord(c.Y * i, c.X * i);
        }

        public static bool operator ==(Coord c1, Coord c2)
        {
            return c1.Equals(c2);
        }

        public static bool operator !=(Coord c1, Coord c2)
        {
            return !c1.Equals(c2);
        }

        public override string ToString()
        {
            return $"({Y},{X})";
        }

        public Coord Move(Coord dir, int mapSize)
        {
            int y = (Y + dir.Y) % mapSize;
            if (y < 0)
                y += mapSize;
            int x = (X + dir.X) % mapSize;
            if (x < 0)
                x += mapSize;
            return new Coord(y, x);
        }
    }
}
