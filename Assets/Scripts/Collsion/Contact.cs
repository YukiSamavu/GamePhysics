using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Contact
{
    public Body bodyA;
    public Body bodyb;
    public float depth;
    public Vector2 normal;
}
