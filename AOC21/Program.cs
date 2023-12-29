namespace AOC21
{
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

            int steps = 64;
            
            var fringe = new Stack<Node>();
            var visited = new HashSet<Node>();
            var finishes = new HashSet<Node>();
            fringe.Push(new Node(startingPos, 0));

            while(fringe.Count > 0)
            {
                var node = fringe.Pop();

                if (visited.Contains(node))
                    continue;
                visited.Add(node);
                if(node.Depth == steps)
                {
                    finishes.Add(node);
                    continue;
                }

                foreach(var neighbor in GetNeighbors(node, map))
                {
                    if(visited.Contains(neighbor))
                        continue;

                    fringe.Push(neighbor);
                }
            }

            Console.WriteLine(finishes.Count());
        }

        static void PrintMap(char[][] map, HashSet<Coord> steps)
        {
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    if(steps.Contains(new Coord(y, x)))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(map[y][x]);
                        Console.ResetColor();
                    } else
                    {
                        Console.Write(map[y][x]);
                    }
                }
                Console.WriteLine();
            }
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
            bool isOnMap = coord.X >= 0 && coord.Y >= 0 && coord.X < map[0].Length && coord.Y < map.Length;
            return isOnMap && map[coord.Y][coord.X] == '.';
        }
    }
}
