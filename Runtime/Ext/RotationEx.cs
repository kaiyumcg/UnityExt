using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RotationEx
{
    public static void ExSetRotationAroundLocalXAxis(this Transform transform, float angle, ref float lastDeltaAngle)
    {
        angle *= -1f;
        Quaternion localRotation = Quaternion.Euler(angle - lastDeltaAngle, 0f, 0f);
        transform.rotation = transform.rotation * localRotation;
        lastDeltaAngle = angle;
    }
    public static void ExSetRotationAroundLocalYAxis(this Transform transform, float angle, ref float lastDeltaAngle)
    {
        angle *= -1f;
        Quaternion localRotation = Quaternion.Euler(0f, angle - lastDeltaAngle, 0f);
        transform.rotation = transform.rotation * localRotation;
        lastDeltaAngle = angle;
    }
    public static void ExSetRotationAroundLocalZAxis(this Transform transform, float angle, ref float lastDeltaAngle)
    {
        angle *= -1f;
        Quaternion localRotation = Quaternion.Euler(0f, 0f, angle - lastDeltaAngle);
        transform.rotation = transform.rotation * localRotation;
        lastDeltaAngle = angle;
    }
}
