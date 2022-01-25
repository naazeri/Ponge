using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncySurface : MonoBehaviour
{
    public float bounceStrength = 20.0f;
    [Range(1f, 5f)]
    public float bounceDistraction = 1f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var ball = collision.gameObject.GetComponent<Ball>();

        if (ball != null)
        {
            var normal = collision.GetContact(0).normal;
            if (Mathf.Abs(normal.x) > 1.0f || Mathf.Abs(normal.y) > 1.0f)
                Debug.Log(normal);
            normal.y *= bounceDistraction;

            ball.AddForce(-normal * bounceStrength);
            PlayBounceSound();
        }
    }

    private void PlayBounceSound()
    {
        var audioManager = FindObjectOfType<AudioManager>();
        audioManager.PlayBouncSound();
    }
}
