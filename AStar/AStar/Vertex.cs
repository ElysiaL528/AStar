using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
    public class Vertex<T> where T : IComparable<T>
    {
        public T Value { get; set; }
        public double CumulativeDistance { get; set; }
        public double FinalDistance { get; set; } //known distance + heuristic
        public Vertex<T> Founder { get; set; }
        public bool WasVisited { get; set; }
        public Dictionary<Vertex<T>, double> Neighbors { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Vertex(T value, int x, int y)
        {
            Value = value;
            X = x;
            Y = y;
            CumulativeDistance = int.MaxValue;
            FinalDistance = int.MaxValue;
            Founder = null;
            WasVisited = false;
            Neighbors = new Dictionary<Vertex<T>, double>();
        }

        public int CompareTo(Vertex<T> vertex)
        {
            if(vertex == null)
            {
                return 1;
            }
            return Value.CompareTo(vertex.Value);
        }
    }
}
