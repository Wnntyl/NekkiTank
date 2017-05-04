using UnityEngine;

public class Utilities
{
    public static Vector4 GetScreenBoundsInWorldSpace()
    {
        var lowerLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        var upperRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        var result = new Vector4(lowerLeft.x, lowerLeft.y, upperRight.x, upperRight.y);

        return result;
    }

    public static float GetScreenWidthInWorldSpace()
    {
        var bounds = GetScreenBoundsInWorldSpace();
        var result = Mathf.Abs(bounds.x) + Mathf.Abs(bounds.z);

        return result;
    }

    public static float GetScreenHeightInWorldSpace()
    {
        var bounds = GetScreenBoundsInWorldSpace();
        var result = Mathf.Abs(bounds.y) + Mathf.Abs(bounds.w);

        return result;
    }
}