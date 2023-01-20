using UnityEngine;

public static class VectorExtensions
{
    public static Vector2 With(this Vector2 original, float? x = null, float? y = null)
    {
        return new Vector2
        (
            x ?? original.x,
            y ?? original.y
        );
    }

    public static Vector3 With(this Vector3 original, float? x = null, float? y = null, float? z = null)
    {
        return new Vector3
        (
            x ?? original.x,
            y ?? original.y,
            z ?? original.z
        );
    }

    public static Vector2 WithMagnitude(this Vector2 original, float magnitude)
    {
        return original.normalized * magnitude;
    }

    public static Vector3 ComponentMultiply(this Vector3 original, float x = 1, float y = 1, float z = 1)
    {
        return new Vector3
        (
            original.x * x,
            original.y * y,
            original.z * z
        );
    }

    public static Vector2 ComponentMultiply(this Vector2 original, float x = 1, float y = 1)
    {
        return new Vector2
        (
            original.x * x,
            original.y * y
        );
    }
}