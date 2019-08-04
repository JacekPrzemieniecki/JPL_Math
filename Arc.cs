using Unity.Mathematics;
using static Unity.Mathematics.math;
using static JPL.Math.JPLMath;
using UnityEngine.Assertions;

namespace JPL.Math
{
    public static class Arc
    {
        public static bool TryFindCenter(float2 v1, float2 v2, float2 tangentAtV1, out float2 center)
        {
            var mid = middle(v1, v2);
            var middleNormal = rotate90cw(normalize(fromTo(v1, v2)));
            var radial = rotate90cw(tangentAtV1);

            if (Intersections.Line2DToLine2D(mid, middleNormal, v1, radial, out var f))
            {
                center = mid + middleNormal * f;
                return true;
            }
            else
            {
                center = default;
                return false;
            }
        }

        public static bool IsClockwise(float2 v1, float2 v2, float2 center)
            => JPLMath.areClockwise(v1 - center, v2 - center);

        public static float Length(float2 v1, float2 v2, float2 center)
        {
            if (isinf(center.x) || isinf(center.y))
            {
                return distance(v1, v2);
            }
            var radius1 = v1 - center;
            var radius2 = v2 - center;
            var r = length(radius1);
            Assert.AreApproximatelyEqual(r, length(radius2));
            var angle = acos(dot(normalize(radius1), normalize(radius2)));
            return r * angle;
        }
    }
}
