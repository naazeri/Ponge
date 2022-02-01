using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScene : BaseMenuScene
{
    void Start()
    {
        title.text = Application.productName;
    }
}
