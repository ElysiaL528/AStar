using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
    public class Heap<T> where T : IComparable<T>
    {
        Vertex<T>[] array;
        public int Count;

        public Heap()
        {
            array = new Vertex<T>[2];
        }

        public object this[int index]
        {
            get
            {
                return array[index];
            }
        }

        public void Insert(Vertex<T> value)
        {
            if (Count >= array.Length - 1)
            {
                //increase array size
                Vertex<T>[] tempArray = new Vertex<T>[array.Length * 2];
                array.CopyTo(tempArray, 0);
                array = tempArray;
            }
            Count++;
            array[Count - 1] = value;
            HeapifyUp(Count - 1);
        }

        public void HeapifyUp(int index)
        {
            //Each node's children are array[2*index + 1] and array[2*index + 2]
            int parentIndex = (index - 1) / 2;
            bool reachedRoot = false;

            while (!reachedRoot)
            {
                if (index == 0)
                {
                    reachedRoot = true;
                    return;
                }
                if (array[index].CompareTo(array[parentIndex]) < 0)
                {
                    var tempNode = array[index];
                    array[index] = array[parentIndex];
                    array[parentIndex] = tempNode;
                }
                index = (parentIndex - 1) / 2;
            }
        }

        public void HeapifyDown(int index)
        {
            if(Count == 0)
            {
                return;
            }

            bool reachedLeaf = false;

            while (!reachedLeaf)
            {
                int leftChildIndex = 2 * index + 1;
                int rightChildIndex = 2 * index + 2;
                int smallerChildIndex;

                if (leftChildIndex > Count - 1) //already at leaf node
                {
                    reachedLeaf = true;
                    return;
                }
                else if (rightChildIndex > Count - 1) //only has left child
                {
                    smallerChildIndex = leftChildIndex;
                }
                else //has two children
                {
                    smallerChildIndex = leftChildIndex < rightChildIndex ? leftChildIndex : rightChildIndex;
                }

                if (array[index].CompareTo(array[smallerChildIndex]) > 0)
                {
                    var temp = array[index];
                    array[index] = array[smallerChildIndex];
                    array[smallerChildIndex] = temp;
                    index++;
                }
                else
                {
                    return;
                }
            }

        }

        public Vertex<T> Pop()
        {
            var root = array[0];
            Count--;
            if (Count > 0)
            {
                array[0] = array[Count];
            }
            else
            {
                array[0] = null;
            }
            array[Count] = null;
            HeapifyDown(0);
            return root;
        }

        public bool Contains(Vertex<T> vertex)
        {
            for(int i = 0; i < Count; i++)
            {
                if(array[i].Value.CompareTo(vertex.Value) == 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
