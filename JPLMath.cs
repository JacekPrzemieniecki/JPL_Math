using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine.Assertions;
using static Unity.Mathematics.math;

namespace JPL.Math
{
    public static class JPLMath
    {
        const float SmallFloat = 1e-5f;
        public static float sqr(float a) => a * a;

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

        public static int indexOfMin<T>(NativeSlice<T> slice) where T: struct, IComparable<T>
        {
            Assert.IsTrue(slice.Length > 0);
            int idx = 0;
            for (int i = 1; i < slice.Length; i++)
            {
                if (slice[idx].CompareTo(slice[i]) > 0)
                {
                    idx = i;
                }
            }
            return idx;
        }
        public static T findMin<T>(NativeSlice<T> slice) where T : struct, IComparable<T> 
            => slice[indexOfMin(slice)];
        public static T findMin<T>(NativeArray<T> arr) where T : struct, IComparable<T>
            => findMin(arr.Slice());

        public static int indexOfMax<T>(NativeSlice<T> slice) where T : struct, IComparable<T>
        {
            Assert.IsTrue(slice.Length > 0);
            int idx = 0;
            for (int i = 1; i < slice.Length; i++)
            {
                if (slice[idx].CompareTo(slice[i]) < 0)
                {
                    idx = i;
                }
            }
            return idx;
        }
        public static T findMax<T>(NativeSlice<T> slice) where T : struct, IComparable<T>
            => slice[indexOfMax(slice)];
        public static T findMax<T>(NativeArray<T> arr) where T : struct, IComparable<T>
            => findMax(arr.Slice());


        public static float average(NativeSlice<float> slice)
        {
            Assert.IsTrue(slice.Length > 0);
            return sum(slice) / slice.Length;
        }
        public static float2 average(NativeSlice<float2> slice)
        {
            Assert.IsTrue(slice.Length > 0);
            return sum(slice) / slice.Length;
        }
        public static float3 average(NativeSlice<float3> slice)
        {
            Assert.IsTrue(slice.Length > 0);
            return sum(slice) / slice.Length;
        }

        public static float sum(NativeSlice<float> slice)
        {
            float s = default;
            for (int i = 0; i < slice.Length; i++)
            {
                s += slice[i];
            }
            return s;
        }
        public static float2 sum(NativeSlice<float2> slice)
        {
            float2 s = default;
            for (int i = 0; i < slice.Length; i++)
            {
                s += slice[i];
            }
            return s;
        }
        public static float3 sum(NativeSlice<float3> slice)
        {
            float3 s = default;
            for (int i = 0; i < slice.Length; i++)
            {
                s += slice[i];
            }
            return s;
        }
        public static int sum(NativeSlice<int> slice)
        {
            int s = 0;
            for (int i = 0; i < slice.Length; i++)
            {
                s += slice[i];
            }
            return s;
        }
        public static int2 sum(NativeSlice<int2> slice)
        {
            int2 s = 0;
            for (int i = 0; i < slice.Length; i++)
            {
                s += slice[i];
            }
            return s;
        }
        public static int3 sum(NativeSlice<int3> slice)
        {
            int3 s = 0;
            for (int i = 0; i < slice.Length; i++)
            {
                s += slice[i];
            }
            return s;
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
