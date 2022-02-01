using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncySurface : MonoBehaviour
{
    public float bounceStrength = 20.0f;
    private float minBounce = 0.0f;
    private float maxBounce = 2.0f;

    [Range(1f, 5f)]
    public float bounceDistraction = 1f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var ball = collision.gameObject.GetComponent<Ball>();

        if (ball != null)
        {
            PlayBounceSound();

            var normal = -collision.GetContact(0).normal;

            // Debug.Log($"Old: X:{normal.x} Y:{normal.y}");

            float x = normal.x;
            float y = normal.y;

            // normal.x = Clamp(normal.x);
            // normal.y = Clamp(normal.y);

            if (normal.x != x || normal.y != y)
                Debug.Log($"Old: X:{x} Y:{y} -- New: X:{normal.x} Y:{normal.y}");

            // float ySign = Mathf.Sign(normal.y);
            normal.y *= bounceDistraction;

            // check if normal <
            ball.AddForce(normal * bounceStrength);
        }
    }

    private void PlayBounceSound()
    {
        var audioManager = FindObjectOfType<AudioManager>();
        audioManager.PlayBouncSound();
    }

    private float Clamp(float v)
    {
        if (v == 0.0f || v == 1.0f || v == -1.0f)
            return v;

        float sign = Mathf.Sign(v);
        float vAbs = Mathf.Abs(v);

        if (vAbs > 0.0f && vAbs < 1.0f)
            return 0.0f;
        else if (vAbs > 1.0f)
            return 1.0f * sign;
        // return Mathf.Clamp(v, -maxBounce, -minBounce);

        return v;

    }
}
