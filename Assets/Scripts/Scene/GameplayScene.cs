using UnityEngine;

public class GameplayScene : MonoBehaviour
{
    private void OnDisable()
    {
        DisablePaddles();
        ShowCursor();
    }

    private void DisablePaddles()
    {
        var paddles = GetComponentsInChildren<Paddle>();

        foreach (var paddle in paddles)
        {
            paddle.gameObject.SetActive(false);
        }
    }

    private void ShowCursor()
    {
        if (!Application.isEditor && !Application.isMobilePlatform)
            CursorHelper.SetVisible(true);
    }
}
