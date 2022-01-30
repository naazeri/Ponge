using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaddle : Paddle
{
    private Vector2 direction;
    [SerializeField] private bool isLeftPlayer = true;

    void Start()
    {

    }
    void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        if (direction.sqrMagnitude != 0)
        {
            _rigidbody.AddForce(direction * this.speed);
        }
    }

    private void HandleInput()
    {
        var isUp = false;
        var isDown = false;

        if (Configs.gameMode == GameMode.OnePlayer)
        {
            isUp = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
            isDown = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        }
        else if (Configs.gameMode == GameMode.TwoPlayer)
        {
            if (isLeftPlayer)
            {
                isUp = Input.GetKey(KeyCode.W);
                isDown = Input.GetKey(KeyCode.S);
            }
            else
            {
                isUp = Input.GetKey(KeyCode.UpArrow);
                isDown = Input.GetKey(KeyCode.DownArrow);
            }
        }

        if (isUp)
        {
            direction = Vector2.up;
        }
        else if (isDown)
        {
            direction = Vector2.down;
        }
        else
        {
            direction = Vector2.zero;
        }
    }

}
