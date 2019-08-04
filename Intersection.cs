using Unity.Mathematics;
using static JPL.Math.JPLMath;
using static Unity.Mathematics.math;

namespace JPL.Math
{
    public static class Intersections
    {
        public static bool Line2DToLine2D(
            float2 origin1,
            float2 dir1,
            float2 origin2,
            float2 dir2,
            out float distanceOnRay1)
        {
            var denomin = perp(dir2, dir1);
            if (abs(denomin) < 0.001f)
            {
                distanceOnRay1 = float.PositiveInfinity;
                return false;
            }
            distanceOnRay1 = (perp(origin1, dir2) + perp(dir2, origin2))
                    / denomin;
            return true;
        }

        public static bool SegmentToSegment(
            float2 origin1,
            float2 dir1,
            float2 origin2,
            float2 dir2,
            out float distOnSegment1,
            out float distOnSegment2)
        {
            // derivation:
            // http://stackoverflow.com/questions/563198/how-do-you-detect-where-two-line-segments-intersect
            var denomin = perp(dir1, dir2);
            if (abs(denomin) < 0.001f)
            {
                distOnSegment1 = 0;
                distOnSegment2 = 0;
                return false;
            }

            var dir2To1 = origin2 - origin1;

            distOnSegment1 = perp(dir2To1, dir2) / denomin;
            distOnSegment2 = perp(dir2To1, dir1) / denomin;
            return distOnSegment1 <= 1 && distOnSegment1 >= 0 && distOnSegment2 <= 1 && distOnSegment2 >= 0;
        }

        public static bool RayToXZPlane(float3 origin, float3 dir, out float distance)
        {
            var dirY = dir.y;
            if (abs(dirY) > 1E-5f)
            {
                distance = -origin.y / dirY;
                return distance >= 0;
            }
            else
            {
                distance = 0;
                return false;
            }
        }

        public static bool RayToXZPlane_Position(float3 origin, float3 dir, out float3 point)
        {
            if (RayToXZPlane(origin, dir, out var dist))
            {
                point = origin + dir * dist;
                return true;
            }
            else
            {
                point = default;
                return false;
            }
        }

        public static bool RayToPlane(float3 origin, float3 dir, float3 planeNormal, float3 pointOnPlane, out float rayDistance)
            => RayToPlane(origin, dir, planeNormal, dot(planeNormal, pointOnPlane), out rayDistance);

        public static bool RayToPlane(float3 origin, float3 dir, float3 planeNormal, float planeDistanceFrom0, out float rayDistance)
        {
            var denom = dot(planeNormal, dir);
            rayDistance = dot(planeDistanceFrom0 * planeNormal - origin, planeNormal) / denom;
            return rayDistance >= 0 && abs(denom) > 1e-5f;
        }

        // rect defined as center, up vector (upper middle - center) and right(middle right - center)
        public static bool RayToRectangle3D(float3 rayOrigin, float3 rayDir, float3 rectCenter, float3 rectUp, float3 rectRight, out float distance)
        {
            if (RayToPlane(rayOrigin, rayDir, normalize(cross(rectUp, rectRight)), rectCenter, out distance))
            {
                var point = rayOrigin + rayDir * distance;
                var localPoint = fromTo(rectCenter, point);
                return abs(dot(rectUp, localPoint)) < lengthsq(rectUp) &&
                       abs(dot(rectRight, localPoint)) < lengthsq(rectRight);
            }
            return false;
        }
    }
}
