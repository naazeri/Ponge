using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HelpScene : BaseMenuScene
{
    private void Start()
    {
        string text;
        if (Application.isMobilePlatform)
        {
            text = "Movement: Touch half Top or Bottom of target paddle's area";
        }
        else
        {
            text = "Movement: WASD or Arrow Keys\nReset Round: R";
        }

        title.text = text;
    }
}
