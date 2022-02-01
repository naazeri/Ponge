using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float speed = 200.0f;

    [SerializeField]
    private float minXVelocity = 6f;
    private float lastMinXVelocity = 0f;
    private Rigidbody2D rigidbody2;

    private void Awake()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Reset();
        AddStartingForce();
    }

    private void FixedUpdate()
    {
        if (IsOutOfScreen())
        {
            Reset();
            AddStartingForce();
        }

        // CheckMinVelocity();
    }

    private void CheckMinVelocity()
    {
        var vel = rigidbody2.velocity;
        float x = vel.x;

        if (x == 0f)
            return;

        // float xAbs = Mathf.Abs(x);

        // if (xAbs < lastMinXVelocity)
        // {
        //     float signX = Mathf.Sign(x);

        //     vel.x = lastMinXVelocity * signX;
        //     rigidbody2.velocity = vel;

        //     Debug.Log($"old: {x}  new:{vel.x}");
        // }
        // else
        // {
        //     lastMinXVelocity = xAbs;
        // }

        if (vel.x > 0.0f)
        {
            if (vel.x < minXVelocity)
            {
                vel.x = minXVelocity;
                rigidbody2.velocity = vel;
                Debug.Log($"old: {x}  new:{vel.x} - Y:{vel.y}");
            }
        }
        else
        {
            if (vel.x > -minXVelocity)
            {
                vel.x = -minXVelocity;
                rigidbody2.velocity = vel;
                Debug.Log($"old: {x}  new:{vel.x} - Y:{vel.y}");
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

    public void Reset()
    {
        rigidbody2.position = Vector2.zero;
        rigidbody2.velocity = Vector2.zero;
        lastMinXVelocity = 0f;
    }

    public void AddStartingForce()
    {
        float x = Random.value < 0.5f ? -1.0f : 1.0f;
        float y = Random.value < 0.5f ? Random.Range(-1.0f, -0.5f) : Random.Range(0.5f, 1.0f); // make random angle

        var direction = new Vector2(x, y);
        rigidbody2.AddForce(direction * speed);
    }
    public void AddForce(Vector2 force)
    {
        rigidbody2.AddForce(force);
    }
}
