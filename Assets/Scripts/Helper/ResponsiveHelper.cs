using UnityEngine;

public enum ScaleType { Width, Height, WidthAndHeight }

public enum VerticalAlign { Top, Middle, Bottom, Current }
public enum HorizontalAlign { Right, Middle, Left, Current }

public class ResponsiveHelper
{
    public static float cameraHeight;
    public static float cameraWidth;
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

    public static void SetPosition(GameObject target, VerticalAlign verticalAlign,
        HorizontalAlign horizontalAlign, float verticalMargin = 0.0f, float horizontalMargin = 0.0f)
    {
        if (!isScreenSizeInitialized)
            UpdateScreenSize();

        var position = target.transform.localPosition;

        position.y = UpdateVerticalPosition(verticalAlign, target, verticalMargin);
        position.x = UpdateHorizontalPosition(horizontalAlign, target, horizontalMargin);

        target.transform.localPosition = position;
    }

    private static float UpdateVerticalPosition(VerticalAlign verticalAlign, GameObject target, float verticalMargin)
    {
        var position = target.transform.localPosition;

        switch (verticalAlign)
        {
            case VerticalAlign.Top:
                position.y = cameraHeight / 2 + target.transform.localScale.y / 2;
                break;

            case VerticalAlign.Middle:
                position.y = 0;
                break;

            case VerticalAlign.Bottom:
                position.y = -(cameraHeight / 2 + target.transform.localScale.y / 2);
                break;

            case VerticalAlign.Current:
            default:
                break;
        }

        position.y += verticalMargin;

        return position.y;
    }

    private static float UpdateHorizontalPosition(HorizontalAlign horizontalAlign, GameObject target, float horizontalMargin)
    {
        var position = target.transform.localPosition;

        switch (horizontalAlign)
        {
            case HorizontalAlign.Right:
                position.x = cameraWidth / 2 + target.transform.localScale.x / 2;
                break;

            case HorizontalAlign.Middle:
                position.x = 0;
                break;

            case HorizontalAlign.Left:
                position.x = -(cameraWidth / 2 + target.transform.localScale.x / 2);
                break;

            case HorizontalAlign.Current:
            default:
                break;
        }

        position.x += horizontalMargin;
        return position.x;
    }

    private static void UpdateScreenSize()
    {
        cameraHeight = Camera.main.orthographicSize * 2.0f;
        cameraWidth = cameraHeight / Screen.height * Screen.width;

        isScreenSizeInitialized = true;
    }
}
