using Unity.Collections;
using static Unity.Mathematics.math;
using static JPL.Math.JPLMath;

namespace JPL.Math
{
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

        public float Get() => average(_buffer.Slice(0, _count));

        public void Dispose() => _buffer.Dispose();
    }
}
