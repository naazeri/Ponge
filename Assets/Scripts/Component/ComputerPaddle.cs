using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerPaddle : Paddle
{
    public Rigidbody2D ball;

    void FixedUpdate()
    {
        if (ball.velocity.x > 0.0f)
        {
            // ball moving right
            if (ball.position.y > transform.position.y)
            {
                AddForceUp();
            }
            else if (ball.position.y < transform.position.y)
            {
                AddForceDown();
            }
        }
        else
        {
            if (transform.position.y > 0.0f)
            {
                AddForceDown();
            }
            else if (transform.position.y < 0.0f)
            {
                AddForceUp();
            }
        }
    }

    private void AddForceUp()
    {
        AddForce(Vector2.up);
    }

    private void AddForceDown()
    {
        AddForce(Vector2.down);
    }

    private void AddForceLeft()
    {
        AddForce(Vector2.left);
    }

    private void AddForceRight()
    {
        AddForce(Vector2.right);
    }

    private void AddForce(Vector2 direction)
    {
        _rigidbody.AddForce(direction * speed);
    }
}
