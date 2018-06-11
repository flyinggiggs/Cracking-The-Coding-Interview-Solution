using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter4___Trees_and_Graphs
{
    public class GraphNode<T> 
    {
        // Private member-variables
        private VisitState _state;
        private T data;
        private List<GraphNode<T>> neighbors = null;
        private List<int> costs;

        public GraphNode(T value) : this(value, null) { }
        public GraphNode(T value, List<GraphNode<T>> neighbors)
        {
            this.data = value;
            this.neighbors = neighbors;
        }

        public VisitState State
        {
            get { return _state; }
            set { _state = value; }
        }

        public T Data
        {
            get { return data; }
            set { data = value; }
        }
        public List<GraphNode<T>> Neighbors
        {
            get
            {
                if (neighbors == null)
                    neighbors = new List<GraphNode<T>>();

                return neighbors;
            }
        }

        public List<int> Costs
        {
            get
            {
                if (costs == null)
                    costs = new List<int>();

                return costs;
            }
        }

        public enum VisitState
        {
            Visited,
            NotVisited
        }
    }
}
