using System;

namespace Lab3;

public class MinHeap<T> where T : IComparable<T>
{
    private T[] array;
    private const int initialSize = 8;

    public int Count { get; private set; }

    public int Capacity => array.Length;

    public bool IsEmpty => Count == 0;


    public MinHeap(T[] initialArray = null)
    {
       array = new T[initialSize];

       if (initialArray == null) return;

       foreach (var item in initialArray)
        {
            Add(item);
        }  
    }

    /// <summary>
    /// Returns the min item but does NOT remove it.
    /// Time complexity: O( 1 )
    /// </summary>
    public T Peek()
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException();
        }

        return array[0];
    }

    // TODO
    /// <summary>
    /// Adds given item to the heap.
    /// Time complexity: O(log(n)) ***BUT*** it might be O(N) if we have to resize
    /// </summary>
    public void Add(T item)
    {
        if (Count == Capacity)
        {
            DoubleArrayCapacity();
        }

        array[Count] = item;
        Count++;

        TrickleUp(Count - 1);
    }

    public T Extract()
    {
        return ExtractMin();
    }

    /// <summary>
    /// Removes and returns the max item in the min-heap.
    /// Time complexity: O( n )
    /// </summary>
    public T ExtractMax()
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException();
        }

        int largest = 0;
        for (int i = 1; i < Count; i++)
        {
            if (array[i].CompareTo(array[largest]) > 0)
            {
                largest = i;
            }
        }

        T largestItem = array[largest];

        Swap(largest, Count - 1);
        array[Count - 1] = default(T);
        Count--;

        RestoreHeap(largest);

        return largestItem;
    }

    // TODO
    /// <summary>
    /// Removes and returns the min item in the min-heap.
    /// Time complexity: O( log(n) )
    /// </summary>
    public T ExtractMin()
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException();
        }

        T min = array[0];

        // swap with last
        Swap(0, Count - 1);

        // remove last
        array[Count - 1] = default(T);
        Count--;

        // trickle down
        if (!IsEmpty)
        {
            TrickleDown(0);
        }

        return min;
    }

    /// <summary>
    /// Returns true if the heap contains the given value; otherwise false.
    /// Time complexity: O( n )
    /// </summary>
    public bool Contains(T value)
    {
        for (int i = 0; i < Count; i++)
        {
            if (array[i].CompareTo(value) == 0)
            {
                return true;
            }
        }

        return false;
    }

    // TODO
    /// <summary>
    /// Updates the first element with the given value from the heap.
    /// Time complexity: O( n )
    /// </summary>
    public void Update(T oldValue, T newValue)
    {
        int index = FindIndexOf(oldValue);
        if (index == -1)
        {
            throw new InvalidOperationException();
        }

        array[index] = newValue;
        RestoreHeap(index);
    }

    // TODO
    /// <summary>
    /// Removes the first element with the given value from the heap.
    /// Time complexity: O( n )
    /// </summary>
    public void Remove(T value)
    {
        int index = FindIndexOf(value);
        if (index == -1)
        {
            throw new InvalidOperationException();
        }

        Swap(index, Count - 1);
        array[Count - 1] = default(T);
        Count--;

        RestoreHeap(index);
    }

    private int FindIndexOf(T value)
    {
        for (int i = 0; i < Count; i++)
        {
            if (array[i].CompareTo(value) == 0)
            {
                return i;
            }
        }

        return -1;
    }

    // TODO
    // Time Complexity: O( log n )
    private void TrickleUp(int index)
    {
        while (index > 0)
        {
            int parentIndex = Parent(index);
            if (array[index].CompareTo(array[parentIndex]) < 0)
            {
                Swap(index, parentIndex);
               index = parentIndex;
            }
            else
            {
                break;
            }

        }
    }

    // TODO
    // Time Complexity: O( log n )
    private void TrickleDown(int index)
    {
        while (true)
        {
            int left = LeftChild(index);
            int right = RightChild(index);
            int smallest = index;
            if (left < Count && array[left].CompareTo(array[smallest]) < 0)
            {
                smallest = left;
            }
            if (right < Count && array[right].CompareTo(array[smallest]) < 0)
            {
                smallest = right;
            }

            if (smallest == index)
            {
                return;
            }

            Swap(index, smallest);
            index = smallest;
        }
    }

    // TODO
    /// <summary>
    /// Gives the position of a node's parent, the node's position in the heap.
    /// </summary>
    private static int Parent(int position)
    {
        return (position - 1) / 2;
    }

    // TODO
    /// <summary>
    /// Returns the position of a node's left child, given the node's position.
    /// </summary>
    private static int LeftChild(int position)
    {
        return 2 * position + 1;
    }

    // TODO
    /// <summary>
    /// Returns the position of a node's right child, given the node's position.
    /// </summary>
    private static int RightChild(int position)
    {
        return 2 * position + 2;
    }

    private void Swap(int index1, int index2)
    {
        var temp = array[index1];

        array[index1] = array[index2];
        array[index2] = temp;
    }

    private void DoubleArrayCapacity()
    {
        Array.Resize(ref array, array.Length * 2);
    }

    private void RestoreHeap(int index)
    {
        if (index < 0 || index >= Count)
        {
            return;
        }

        if (index > 0 && array[index].CompareTo(array[Parent(index)]) < 0)
        {
            TrickleUp(index);
        }
        else
        {
            TrickleDown(index);
        }
    }
}
