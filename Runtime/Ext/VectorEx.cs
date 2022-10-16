using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityExt
{
    public static class VectorEx
    {
        public static Vector2 ExGetIntersectionPointCoordinates(this Vector2 thisLinePt1, Vector2 thisLinePt2, Vector2 otherLinePt1, Vector2 otherLinePt2, out bool found)
        {
            float tmp = (otherLinePt2.x - otherLinePt1.x) * (thisLinePt2.y - thisLinePt1.y) - (otherLinePt2.y - otherLinePt1.y) * (thisLinePt2.x - thisLinePt1.x);
            if (tmp == 0)
            {
                found = false;
                return Vector2.zero;
            }

            float mu = ((thisLinePt1.x - otherLinePt1.x) * (thisLinePt2.y - thisLinePt1.y) - (thisLinePt1.y - otherLinePt1.y) * (thisLinePt2.x - thisLinePt1.x)) / tmp;
            found = true;
            return new Vector2(
                otherLinePt1.x + (otherLinePt2.x - otherLinePt1.x) * mu,
                otherLinePt1.y + (otherLinePt2.y - otherLinePt1.y) * mu
            );
        }
        public static Vector3 ExGetMidPoint(this Vector3 pt, Vector3 target)
        {
            var toTarget = target - pt;
            var midDist = toTarget.magnitude * 0.5f;
            return pt + toTarget.normalized * midDist;
        }
        public static bool ExHasPassedPoint(this Vector3 thisPt, Vector3 point, Vector3 moveDirection)
        {
            var toPointDirection = point - thisPt;
            return Vector3.Dot(toPointDirection.normalized, moveDirection.normalized) < 0.0f;
        }
        public static Vector3 ExGetPointAtNormalizedDistance(this Vector3 pt, float normalizedDistance, Vector3 target)
        {
            var toTarget = target - pt;
            var dist = toTarget.magnitude * normalizedDistance;
            return pt + toTarget.normalized * dist;
        }
    }
}