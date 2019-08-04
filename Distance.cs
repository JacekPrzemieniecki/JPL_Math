using Unity.Mathematics;
using static JPL.Math.JPLMath;
using static Unity.Mathematics.math;

namespace JPL.Math
{
    public static class DistanceBetween
    {
        public static float PointAndLine(float2 point, float2 lineOrigin, float2 lineDirNormalized) 
            => abs(dot(rotate90cw(lineDirNormalized), fromTo(lineOrigin, point)));
    }
}
