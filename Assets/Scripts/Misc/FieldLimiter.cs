using UnityEngine;

public class FieldLimiter : MonoBehaviour
{
    private const float THICKNESS = 1f;

    private BoxCollider2D _collider;

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        Vector4 param;

        switch(gameObject.name)
        {
            case "Top":
                param.x = 0f;
                param.y = Utilities.GetScreenBoundsInWorldSpace().w;
                param.z = Utilities.GetScreenWidthInWorldSpace();
                param.w = THICKNESS;

                SetParameters(param);
                break;
            case "Right":
                param.x = Utilities.GetScreenBoundsInWorldSpace().z;
                param.y = 0f;
                param.z = THICKNESS;
                param.w = Utilities.GetScreenHeightInWorldSpace();

                SetParameters(param);
                break;
            case "Bottom":
                param.x = 0f;
                param.y = Utilities.GetScreenBoundsInWorldSpace().y;
                param.z = Utilities.GetScreenWidthInWorldSpace();
                param.w = THICKNESS;

                SetParameters(param);
                break;
            case "Left":
                param.x = Utilities.GetScreenBoundsInWorldSpace().x;
                param.y = 0f;
                param.z = THICKNESS;
                param.w = Utilities.GetScreenHeightInWorldSpace();

                SetParameters(param);
                break;
        }
    }

    private void SetParameters(Vector4 param)
    {
        var pos = new Vector2(param.x, param.y);
        var size = new Vector2(param.z, param.w);

        transform.position = pos;
        _collider.size = size;
    }
}