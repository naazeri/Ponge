using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CursorHelper //: MonoBehaviour
{
    public static void SetVisible(bool visible)
    {
        if (visible)
        {
            // Cursor.lockState = CursorLockMode.Locked;

        }
        else
        {
            // Cursor.lockState = CursorLockMode.Locked;
        }

        Cursor.visible = visible;
    }
}
