namespace Part2
{
    // For future reference
    // This only calculates the point values every 131 steps + answer % map.Length
    // The points resolves to a quadratic formula
    // Later wrote the rest in C#

    internal class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            char[][] map = lines.Select(l => l.ToCharArray()).ToArray();

            Coord startingPos = new Coord(-1, -1);
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    if (map[y][x] == 'S')
                    {
                        map[y][x] = '.';
                        startingPos = new Coord(y, x);
                        break;
                    }
                }
            }

            int steps = 26501365;

            int modded = steps % map.Length;

            int lastWritten = 0;

            var fringe = new Queue<Node>();
            var visited = new HashSet<Node>();
            fringe.Enqueue(new Node(startingPos, 0));

            var nodesCount = new Dictionary<int, int>();

            while (fringe.Count > 0)
            {
                var node = fringe.Dequeue();
                if (visited.Contains(node))
                    continue;
                if ((node.Depth - modded) % map.Length == 0)
                {
                    int count;
                    nodesCount.TryGetValue(node.Depth, out count);
                    nodesCount[node.Depth] = count + 1;
                }

                if ((node.Depth - modded) % map.Length == 1 && lastWritten != node.Depth - 1)
                {
                    lastWritten = node.Depth - 1;
                    Console.WriteLine($"{lastWritten}: {nodesCount[lastWritten]}");
                    if (nodesCount.Count == 3)
                        break;
                }

                visited.Add(node);

                foreach (var neighbor in GetNeighbors(node, map))
                {
                    if (visited.Contains(neighbor))
                        continue;

                    fringe.Enqueue(neighbor);
                }
            }
            var yVals = nodesCount.Values.ToList();
            yVals.Sort();
            var formula = QuadraticFormula.GetQuadraticFormula(new Coord(yVals[0], 0), new Coord(yVals[1], 1), new Coord(yVals[2], 2));
            Console.WriteLine(formula);
            long res = formula.GetY((steps - modded) / map.Length);
            Console.WriteLine($"Result: {res}");
        }

        static Node[] GetNeighbors(Node node, char[][] map)
        {
            var res = new List<Node>();
            foreach (int dir in new int[] { 1, -1 })
            {
                Coord move = new Coord(dir, 0);
                if (IsValid(node.Pos + move, map))
                    res.Add(new Node(node.Pos + move, node.Depth + 1));

                move = new Coord(0, dir);
                if (IsValid(node.Pos + move, map))
                    res.Add(new Node(node.Pos + move, node.Depth + 1));
            }
            return res.ToArray();
        }

        static bool IsValid(Coord coord, char[][] map)
        {
            int x = coord.X;
            while (x < 0)
            {
                x += map.Length;
            }
            int y = coord.Y;
            while (y < 0)
            {
                y += map.Length;
            }
            return map[y % map.Length][x % map.Length] == '.';
        }

    }
}
