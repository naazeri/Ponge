using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float speed = 300.0f;
    private float maxVelocity = 10f;
    private float minXVelocity;
    // private float lastMinXVelocity = 0f;
    private Rigidbody2D rigidbody2;

    private void Awake()
    {
        minXVelocity = speed / 55f;
        rigidbody2 = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
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

        ClampVelocity();
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

    private void ClampVelocity()
    {
        var v = rigidbody2.velocity;

        v.x = limitToMin(v.x);

        // Debug.Log(Vector2.SqrMagnitude(v));
        // Debug.Log(Vector2.ClampMagnitude(v));
        v.x = limitToMax(v.x);
        v.y = limitToMax(v.y);

        rigidbody2.velocity = v;
    }

    private float limitToMin(float value)
    {
        if (value > 0.0f && value < minXVelocity)
        {
            value = minXVelocity;
        }
        else if (value < 0.0f && value > -minXVelocity)
        {
            value = -minXVelocity;
        }

        return value;
    }
    private float limitToMax(float value)
    {
        if (value > 0.0f && value > maxVelocity)
        {
            value = maxVelocity;
        }
        else if (value < 0.0f && value < -maxVelocity)
        {
            value = -maxVelocity;
        }

        return value;
    }

    public void Reset()
    {
        rigidbody2.position = Vector2.zero;
        rigidbody2.velocity = Vector2.zero;
        // lastMinXVelocity = 0f;
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
