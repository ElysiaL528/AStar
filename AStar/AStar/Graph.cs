using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
    public class Graph<T> where T : IComparable<T>
    {
        public List<Vertex<T>> Vertices { get; set; }
        Func<Vertex<T>, Vertex<T>, double> Heuristics;

        public Graph(Func<Vertex<T>, Vertex<T>, double> heuristicsFunction)
        {
            Vertices = new List<Vertex<T>>();
            Heuristics = heuristicsFunction;
        }

        public void AddVertex(T value, int x, int y)
        {
            var vertex = new Vertex<T>(value, x, y);
            Vertices.Add(vertex);
        }

        public void AddEdge(Vertex<T> start, Vertex<T> end, double weight)
        {
            if(start.Neighbors.ContainsKey(end))
            {
                return;
            }
            start.Neighbors.Add(end, weight);
        }

        public void RemoveVertex(Vertex<T> vertex)
        {
            if(!Vertices.Contains(vertex))
            {
                return;
            }
            Vertices.Remove(vertex);
        }

        public void RemoveEdge(Vertex<T> start, Vertex<T> end)
        {
            if(!start.Neighbors.ContainsKey(end))
            {
                return;
            }
            start.Neighbors.Remove(end);
        }

        public Vertex<T> AStar(Vertex<T> start, Vertex<T> goal)
        {
            var priorityQueue = new Heap<T>();

            start.CumulativeDistance = 0;
            start.FinalDistance = Heuristics(start, goal);
            priorityQueue.Insert(start);

            while(priorityQueue.Count > 0 && !goal.WasVisited)
            {
                Vertex<T> vertex = priorityQueue.Pop();

                foreach (var neighbor in vertex.Neighbors.Keys)
                {
                    if (!neighbor.WasVisited)
                    {
                        var tentativeDistance = vertex.CumulativeDistance + vertex.Neighbors[neighbor];
                        if(tentativeDistance < neighbor.CumulativeDistance)
                        {
                            neighbor.CumulativeDistance = tentativeDistance;
                            neighbor.Founder = vertex;
                            neighbor.FinalDistance = neighbor.CumulativeDistance + Heuristics(neighbor, goal);
                            neighbor.WasVisited = false;
                        }
                        if (!priorityQueue.Contains(neighbor))
                        { 
                            priorityQueue.Insert(neighbor);
                        }
                    }
                }
                vertex.WasVisited = true;
            }
            return goal;
        }
    }
}
