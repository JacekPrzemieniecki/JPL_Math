using System;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using static Unity.Mathematics.math;

namespace JPL.Math
{

    public static class JPLMath
    {
        const float SmallFloat = 1e-5f;
        public static void minmax(float a, float b, out float min, out float max)
        {
            if (a > b)
            {
                min = b;
                max = a;
            }
            else
            {
                min = a;
                max = b;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ceilint(float a)
            => (int)ceil(a);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int2 ceilint(float2 a)
            => (int2)ceil(a);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int3 ceilint(float3 a)
            => (int3)ceil(a);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int floorint(float a)
            => (int)floor(a);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int2 floorint(float2 a)
            => (int2)floor(a);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int3 floorint(float3 a)
            => (int3)floor(a);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float middle(float a, float b)
            => (a + b) / 2;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 middle(float2 a, float2 b)
            => (a + b) / 2;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 middle(float3 a, float3 b)
            => (a + b) / 2;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float fromTo(float from, float to)
            => (to - from);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 fromTo(float2 from, float2 to)
            => (to - from);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 fromTo(float3 from, float3 to)
            => (to - from);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float perp(float2 a, float2 b)
            => a.x * b.y - a.y * b.x;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float cross(float2 a, float2 b)
            => perp(a, b);

        /// <summary> true if a has a shorter way to cover rotating clockwise to reach b </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool areClockwise(float2 a, float2 b)
            => perp(a, b) < 0;

        public static float2 rotate(float2 v, float angle)
        {
            sincos(angle, out float s, out float c);
            return float2(
                c * v.x - s * v.y,
                s * v.x + c * v.y);
        }

        public static float2 rotate90cw(float2 v)
            => float2(-v.y, v.x);
        public static float2 rotate90ccw(float2 v)
            => float2(v.y, -v.x);

        public static float3 rotateX(float3 v, float angle)
        {
            var r = rotate(v.yz, angle);
            return float3(v.x, r);
        }

        public static float3 rotateY(float3 v, float angle)
        {
            var r = rotate(v.zx, angle);
            return float3(r.y, v.y, r.x);
        }

        public static float3 rotateZ(float3 v, float angle)
        {
            var r = rotate(v.xy, angle);
            return float3(r, v.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool equal<T>(T v1, T v2) where T : IEquatable<T>
            => v1.Equals(v2);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool equal<T>(T v1, T v2, T v3) where T : IEquatable<T>
            => equal(v1, v2) && equal(v2, v3);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool equal<T>(T v1, T v2, T v3, T v4) where T : IEquatable<T>
            => equal(v1, v2, v3) && equal(v3, v4);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool equal<T>(T v1, T v2, T v3, T v4, T v5) where T : IEquatable<T>
            => equal(v1, v2, v3, v4) && equal(v4, v5);
    }
}
