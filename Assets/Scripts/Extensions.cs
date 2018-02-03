using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{

    public static Vector3 Multiply(this Vector3 vec, Vector3 other)
    {
        vec.Scale(other);
        return vec;
    }

    public static float Sum(this Vector3 vec)
    {
        return vec.x + vec.y + vec.z;
    }

}
