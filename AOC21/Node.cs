using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC21
{
    internal class Node
    {
        public Coord Pos { get; set; }
        public int Depth { get; set; }

        public Node(Coord pos, int depth)
        {
            Pos = pos;
            Depth = depth;
        }

        public override bool Equals(object? obj)
        {
            return obj is Node node &&
                   Pos.Equals(node.Pos) &&
                   Depth == node.Depth;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Pos, Depth);
        }
    }
}
