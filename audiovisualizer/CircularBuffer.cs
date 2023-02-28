using System;

//CircularBuffer stores last color values and calculate average value. It is necessary for smoothing
public class CircularBuffer
{
    int[] _buffer;
    int _head;
    int _tail;
    int _length;
    int _bufferSize;
    Object _lock = new object();

    public CircularBuffer(int bufferSize)
    {
        _buffer = new int[bufferSize];
        _bufferSize = bufferSize;
        _head = bufferSize - 1;
    }

    public bool IsEmpty
    {
        get { return _length == 0; }
    }

    public bool IsFull
    {
        get { return _length == _bufferSize; }
    }

    public int Dequeue()
    {
        lock (_lock)
        {
            if (IsEmpty) throw new InvalidOperationException("Queue exhausted");

            int dequeued = _buffer[_tail];
            _tail = NextPosition(_tail);
            _length--;
            return dequeued;
        }
    }

    private int NextPosition(int position)
    {
        return (position + 1) % _bufferSize;
    }

    public void Enqueue(int toAdd)
    {
        lock (_lock)
        {
            _head = NextPosition(_head);
            _buffer[_head] = toAdd;
            if (IsFull)
                _tail = NextPosition(_tail);
            else
                _length++;
        }
    }

    public int GetAverage()
    {
        int average = 0;
        for (int i = 0; i < _length; i++)
        {
            average += _buffer[i];
        }
        return average / _length;
    }
}
