using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public BoolData simulate;
    public FloatData gravity;
    public FloatData size;

    static World instance;
    static public World Instance { get { return instance; } }
    public Vector2 Gravity { get { return new Vector2(0, gravity.value); } }

    public List<Body> bodies { get; set; } = new List<Body>();

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (!simulate.value) return;
        
        float dt = Time.deltaTime;

        bodies.ForEach(body => body.Step(dt));
        bodies.ForEach(body => Intergrator.SemiImplicitEuler(body, dt));

        bodies.ForEach(body => body.transform.localScale = new Vector3(size.value, size.value, size.value));

        bodies.ForEach(body => body.force = Vector2.zero);
        bodies.ForEach(body => body.acceleration = Vector2.zero);
    }
}
