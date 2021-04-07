using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    public Vector2 force { get; set; } = Vector2.zero;
    public Vector2 position { get { return transform.position; } set { transform.position = value; } }
    public Vector2 velocity { get; set; } = Vector2.zero;
    public Vector2 acceleration { get; set; } = Vector2.zero;
    public float mass { get; set; } = 1;
    public float damping { get; set; } = 0;
    public BoolData randomColor;
    float timer = .01f;
    float colorTimer = 0;

    public void AddForce(Vector2 force)
    {
        this.force += force;
    }

    public void Step(float dt)
    {
        if (randomColor)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

            if(timer <= colorTimer)
            {
                spriteRenderer.color = new Color(Random.Range(0, 1f) * 255, Random.Range(0, 1f) * 255, Random.Range(0, 1f) * 255, 1);
                colorTimer = 0;
            }
        }
        colorTimer += dt;
        acceleration = World.Instance.Gravity + (force / mass);
    }
}
