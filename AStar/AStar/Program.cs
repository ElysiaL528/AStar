using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
    class Program
    {
        public static double Manhattan(Vertex<string> start, Vertex<string> goal)
        {
            //Manhattan
            double scalar = 1;
            double xDistance = Math.Abs(start.X - goal.X);
            double yDistance = Math.Abs(start.Y - goal.Y);

            return scalar * (xDistance + yDistance);
        }

        public static double Diagonal(Vertex<string> start, Vertex<string> goal)
        {
            double scalar = 1;
            double secondScalar = Math.Sqrt(2);
            double dx = Math.Abs(start.X - goal.X);
            double dy = Math.Abs(start.Y - goal.Y);
            return scalar * (dx + dy) + (secondScalar - 2 * scalar) * Math.Min(dx, dy);
        }

        public static double Euclidean(Vertex<string> start, Vertex<string> goal)
        {
            double scalar = 1;
            double dx = Math.Abs(start.X - goal.X);
            double dy = Math.Abs(start.Y - goal.Y);

            return scalar * Math.Sqrt(dx * dx + dy * dy);
        }

        static void Main(string[] args)
        {

            Graph<string> locations = new Graph<string>(Manhattan);
            int maxYcoord = 0;

            //Add vertices
            #region
            string[] letters = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T" };
            int lettersIndex = 0;

            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    locations.AddVertex(letters[lettersIndex], x, y);
                    lettersIndex++;
                }
                maxYcoord++;
            }
            #endregion
            //Add edges
            #region
            for (int i = 0; i < locations.Vertices.Count; i++)
            {
                //Creating left-to-right edges
                if (i < locations.Vertices.Count - 1 && locations.Vertices[i + 1].Y == locations.Vertices[i].Y)
                {
                    locations.AddEdge(locations.Vertices[i], locations.Vertices[i + 1], 1);
                }
                //Creating right-to-left edges
                if (i > 0 && locations.Vertices[i - 1].Y == locations.Vertices[i].Y)
                {
                    locations.AddEdge(locations.Vertices[i], locations.Vertices[i - 1], 1);
                }

                //Create top-to-bottom edges
                if (locations.Vertices[i].Y < maxYcoord - 1 && locations.Vertices[i].X == locations.Vertices[i + 5].X)
                {
                    var southNeighborIndex = i + 1;
                    while (locations.Vertices[southNeighborIndex].Y < 4 && locations.Vertices[i].X != locations.Vertices[southNeighborIndex].X)
                    {
                        southNeighborIndex++;
                    }
                    locations.AddEdge(locations.Vertices[i], locations.Vertices[southNeighborIndex], 1);
                }
                //Create bottom-to-top edges
                if (locations.Vertices[i].Y > 0 && locations.Vertices[i].X == locations.Vertices[i - 5].X)
                {
                    var northNeighborIndex = i - 1;
                    while (locations.Vertices[northNeighborIndex].Y >= 0 && locations.Vertices[i].X != locations.Vertices[northNeighborIndex].X)
                    {
                        northNeighborIndex--;
                    }
                    locations.AddEdge(locations.Vertices[i], locations.Vertices[northNeighborIndex], 1);
                }
            }
            #endregion

            //Grid is 11 x 3

            int finalYVal = 0;

            foreach (var location in locations.Vertices)
            {
                Console.CursorLeft = location.X * 10;
                Console.CursorTop = location.Y * 5;
                finalYVal = location.Y * 5;

                Console.Write(location.Value);
            }

            var finalVertex = locations.AStar(locations.Vertices[0], locations.Vertices[locations.Vertices.Count - 1]);

            var temp = finalVertex;

            Console.ForegroundColor = ConsoleColor.Red;

            while(temp != null)
            {
                Console.CursorLeft = temp.X * 10;
                Console.CursorTop = temp.Y * 5;
                Console.Write($"{temp.Value}");
                temp = temp.Founder;
            }

            Console.ReadKey();
        }
    }
}
