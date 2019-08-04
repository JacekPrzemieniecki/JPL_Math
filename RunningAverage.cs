using Unity.Collections;
using Unity.Mathematics;
using static Unity.Mathematics.math;
using System.Collections.Generic;
using UnityEngine;

public struct RunningAverage
{
    NativeArray<float> _buffer;
    int _count;
    int _next;
    public RunningAverage(int frames, Allocator allocator)
    {
        _buffer = new NativeArray<float>(frames, allocator);
        _count = 0;
        _next = 0;
    }

    public void Add(float val)
    {
        _buffer[_next] = val;
        _count = min(_count + 1, _buffer.Length);
        _next = (_next + 1) % _buffer.Length;
    }

    public float Get()
    {
        if (_count == 0) return 0;
        float sum = 0;
        for (int i = 0; i < _count; i++)
        {
            sum += _buffer[i];
        }
        return sum / _count;
    }

    public void Dispose()
    {
        _buffer.Dispose();
    }
}
