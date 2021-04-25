using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Intergrator
{
    public static void ExplicitEuler(Body body, float dt)
    {
        body.position += (body.velocity * dt);
        body.velocity += (body.acceleration * dt);
        body.velocity *= (1f / (1f + (body.damping * dt)));
    }

    public static void SemiImplicitEuler(Body body, float dt)
    {
        body.velocity += (body.acceleration * dt);
        body.position += (body.velocity * dt);
        body.velocity *= (1f / (1f + (body.damping * dt)));
    }
}
