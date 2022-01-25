using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 200.0f;
    public float minXVelocity = 5f;
    private Rigidbody2D rigidbody2;

    private void Awake()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        ResetPosition();
        AddStartingForce();
    }

    private void FixedUpdate()
    {
        if (IsOutOfScreen())
        {
            ResetPosition();
            AddStartingForce();
        }

        CheckMinVelocity();
    }

    private void CheckMinVelocity()
    {
        var vel = rigidbody2.velocity;

        if (vel.x == 0f)
            return;

        if (vel.x > 0)
        {
            if (vel.x < minXVelocity)
            {
                vel.x = minXVelocity;
                rigidbody2.velocity = vel;
            }
        }
        else
        {
            if (vel.x > -minXVelocity)
            {
                vel.x = -minXVelocity;
                rigidbody2.velocity = vel;
            }
        }
    }

    private bool IsOutOfScreen()
    {
        var x = transform.localPosition.x;
        var y = transform.localPosition.y;

        return x > ResponsiveHelper.cameraWidth ||
            x < -ResponsiveHelper.cameraWidth ||
            y > ResponsiveHelper.cameraHeight ||
            y < -ResponsiveHelper.cameraHeight;
    }

    public void ResetPosition()
    {
        rigidbody2.position = Vector2.zero;
        rigidbody2.velocity = Vector2.zero;
    }

    public void AddStartingForce()
    {
        float x = Random.value < 0.5f ? -1.0f : 1.0f;
        float y = Random.value < 0.5f ? Random.Range(-1.0f, -0.5f) : Random.Range(0.5f, 1.0f); // make random angle

        var direction = new Vector2(x, y);
        rigidbody2.AddForce(direction * speed);

        // minVelocity = rigidbody2.velocity;
    }
    public void AddForce(Vector2 force)
    {
        rigidbody2.AddForce(force);
    }
}
