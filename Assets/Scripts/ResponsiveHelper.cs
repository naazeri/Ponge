using UnityEngine;

public enum ScaleType { Width, Height, WidthAndHeight }

public enum VerticalAlign { Top, Middle, Bottom, Current }
public enum HorizontalAlign { Right, Middle, Left, Current }

public class ResponsiveHelper
{
    private static float cameraHeight;
    private static float cameraWidth;
    private static bool isScreenSizeInitialized = false;


    public static void FixScale(GameObject target, ScaleType scaleType)
    {
        var spriteRenderer = target.GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
            return;

        float width = spriteRenderer.sprite.bounds.size.x;
        float height = spriteRenderer.sprite.bounds.size.y;

        if (!isScreenSizeInitialized)
            UpdateScreenSize();

        var scale = target.transform.localScale;

        switch (scaleType)
        {
            case ScaleType.Width:
                scale.x = cameraWidth / width;
                break;

            case ScaleType.Height:
                scale.y = cameraHeight / height;
                break;

            case ScaleType.WidthAndHeight:
                scale.x = cameraWidth / width;
                scale.y = cameraHeight / height;
                break;
        }

        target.transform.localScale = scale;
    }

    public static void SetPosition(GameObject target, VerticalAlign verticalAlign, HorizontalAlign horizontalAlign)
    {
        if (!isScreenSizeInitialized)
            UpdateScreenSize();

        var position = target.transform.localPosition;

        switch (verticalAlign)
        {
            case VerticalAlign.Top:
                position.y = cameraHeight / 2;
                break;

            case VerticalAlign.Middle:
                position.y = 0;
                break;

            case VerticalAlign.Bottom:
                position.y = -cameraHeight / 2;
                break;

            case VerticalAlign.Current:
            default:
                break;
        }

        switch (horizontalAlign)
        {
            case HorizontalAlign.Left:
                position.x = cameraWidth / 2;
                break;

            case HorizontalAlign.Middle:
                position.x = 0;
                break;

            case HorizontalAlign.Right:
                position.x = -cameraWidth / 2;
                break;

            case HorizontalAlign.Current:
            default:
                break;

        }

        target.transform.localPosition = position;
    }

    private static void UpdateScreenSize()
    {
        cameraHeight = Camera.main.orthographicSize * 2.0f;
        cameraWidth = cameraHeight / Screen.height * Screen.width;

        isScreenSizeInitialized = true;
    }
}
