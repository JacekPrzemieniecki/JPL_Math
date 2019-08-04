using Unity.Mathematics;
using static JPL.Math.JPLMath;
using static Unity.Mathematics.math;

namespace JPL.Math
{
    public static class PointIn
    {
        public static bool Triangle(float2 point, float2 v1, float2 v2, float2 v3)
            => areClockwise(fromTo(v1, point), fromTo(v1, v2))
            && areClockwise(fromTo(v2, point), fromTo(v2, v3))
            && areClockwise(fromTo(v3, point), fromTo(v3, v2));

        // v1, v2, v3, v4 should be in counter-clockwise order
        public static bool Quad(float2 point, float2 q1, float2 q2, float2 q3, float2 q4)
            => equal(
                sign(perp(fromTo(q1, point), fromTo(q1, q2))),
                sign(perp(fromTo(q2, point), fromTo(q2, q3))),
                sign(perp(fromTo(q3, point), fromTo(q3, q4))),
                sign(perp(fromTo(q4, point), fromTo(q4, q1))));


        public static bool Rect(float2 point, float2 rectMin, float2 rectMax)
            => Rect(point, rectMax.y, rectMax.x, rectMin.y, rectMin.x);

        public static bool Rect(float2 point, float top, float right, float bottom, float left)
            => point.x < right && point.x > left &&
               point.y < top && point.y > bottom;

        public static bool Circle(float2 point, float2 center, float radius)
            => lengthsq(fromTo(point, center)) < radius * radius;
    }
}
