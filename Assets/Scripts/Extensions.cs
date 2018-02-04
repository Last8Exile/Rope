using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Extensions
{
    public static void RemoveChilds(this Transform transform)
    {
        foreach (Transform child in transform)
            GameObject.Destroy(child.gameObject);
    }

    public static void SortChilds(this Transform transform)
    {
        var list = new List<Transform>(transform.childCount);
        list.AddRange(transform.Cast<Transform>());
        transform.DetachChildren();
        list.OrderBy(x => x.gameObject.name).ForEach(x => x.SetParent(transform));
    }

    public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
    {
        foreach (var item in collection)
            action(item);
    }

    public static bool HasFlag(this Enum variable, Enum value)
    {
        var num = Convert.ToUInt16(value);
        return (Convert.ToUInt16(variable) & num) == num;
    }

    public static int Pow(int value, int degree)
    {
        if (degree < 0)
            throw new ArgumentOutOfRangeException("degree", "degree должен быть не меньше нуля");
        if (degree == 0)
            return 1;
        if (degree == 1)
            return value;

        var x = Pow(value, degree / 2);
        if (degree % 2 == 1)
            return value * x * x;
        return x * x;
    }

    #region VectorStuff

    public static Vector4 ToVector4(this Vector3 vector, float value = 0)
    {
        return new Vector4(vector.x, vector.y, vector.z, value);
    }

    public static Vector3 ToVector3(this Vector4 vector, Axis sacrifise = Axis.w)
    {
        switch (sacrifise)
        {
            case Axis.x:
                return new Vector3(vector.y, vector.z, vector.w);
            case Axis.y:
                return new Vector3(vector.x, vector.z, vector.w);
            case Axis.z:
                return new Vector3(vector.x, vector.y, vector.w);
            case Axis.w:
                return new Vector3(vector.x, vector.y, vector.z);
        }
        throw new UnityException();
    }

    public static Vector3 ToVector3(this Vector2 vector, float value = 0)
    {
        return new Vector3(vector.x, vector.y, value);
    }

    public static Vector2 ToVector2(this Vector3 vector, Axis sacrifise = Axis.z)
    {
        switch (sacrifise)
        {
            case Axis.x:
                return new Vector2(vector.y, vector.z);
            case Axis.y:
                return new Vector2(vector.x, vector.z);
            case Axis.z:
                return new Vector2(vector.x, vector.y);
        }
        throw new UnityException();
    }

    public static Vector3 Change(this Vector3 vec, Axis axis, float value)
    {
        switch (axis)
        {
            case Axis.x:
                vec.x = value;
                break;
            case Axis.y:
                vec.y = value;
                break;
            case Axis.z:
                vec.z = value;
                break;
        }
        return vec;
    }

    public static Vector4 Change(this Vector4 vec, Axis axis, float value)
    {
        switch (axis)
        {
            case Axis.x:
                vec.x = value;
                break;
            case Axis.y:
                vec.y = value;
                break;
            case Axis.z:
                vec.z = value;
                break;
            case Axis.w:
                vec.w = value;
                break;
        }
        return vec;
    }

    public static float Get(this Vector3 vec, Axis axis)
    {
        switch (axis)
        {
            case Axis.x:
                return vec.x;
            case Axis.y:
                return vec.y;
            case Axis.z:
                return vec.z;
        }
        throw new ArgumentException("Support only x,y,z axes");
    }

    public static Vector3 ScreenToWorldPos(Vector2 point)
    {
        return Camera.main.ScreenToWorldPoint(point.ToVector3());
    }

    public static Vector3 Multiply(this Vector3 vec, Vector3 other)
    {
        vec.Scale(other);
        return vec;
    }

    public static float Sum(this Vector3 vec)
    {
        return vec.x + vec.y + vec.z;
    }

    public static Vector3 Swap(this Vector3 vec, Axis first, Axis second)
    {
        if (first == second)
            return vec;

        var value = vec.Get(first);
        vec = vec.Change(first, vec.Get(second));
        return vec.Change(second, value);
    }

    #endregion
}

public enum Axis
{
    x,
    y,
    z,
    w
}
