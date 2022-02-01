using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaddle : Paddle
{
    [SerializeField] private bool isLeftPlayer = true;
    private Vector2 direction;

    private void Awake()
    {
        if (Application.isMobilePlatform)
        {
            TouchButtonEvents.OnButtonClick += OnTouchListener;
        }
    }

    void Update()
    {
        if (!Application.isMobilePlatform)
        {
            HandleInput();
        }
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

    private void OnTouchListener(ButtonPosition buttonPosition, ButtonClickStatus buttonClickStatus)
    {
        if (isLeftPlayer)
        {
            if (buttonPosition == ButtonPosition.TopLeft)
            {
                ChangeDirection(Vector2.up, buttonClickStatus);
            }
            else if (buttonPosition == ButtonPosition.BottomLeft)
            {
                ChangeDirection(Vector2.down, buttonClickStatus);
            }
        }
        else
        {
            if (buttonPosition == ButtonPosition.TopRight)
            {
                ChangeDirection(Vector2.up, buttonClickStatus);
            }
            else if (buttonPosition == ButtonPosition.BottomRight)
            {
                ChangeDirection(Vector2.down, buttonClickStatus);
            }
        }
    }

    private void ChangeDirection(Vector2 newDirection, ButtonClickStatus buttonClickStatus)
    {
        if (buttonClickStatus == ButtonClickStatus.OnPointerDown)
            this.direction = newDirection;
        else
            this.direction = Vector2.zero;
    }
}
