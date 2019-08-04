using Unity.Mathematics;
using UnityEngine;
using static Unity.Mathematics.math;

namespace JPL
{
    public static class Rotation2D
    {
        // Equivalent to Quaternion.FromToRotation(left, (heading.x, 0, heading.y))
        // except this doesn't blow up when heading ~= left and uses no transcendentals
        public static quaternion HeadingToQuaternion(float2 heading)
        {
            if (heading.y < -0.99f)
            {
                return quaternion(0, 1, 0, 0);
            }
            var norm = sqrt(2 + 2 * heading.y);
            return quaternion(0, heading.x / norm, 0, norm * 0.5f);
        }
    }
}
