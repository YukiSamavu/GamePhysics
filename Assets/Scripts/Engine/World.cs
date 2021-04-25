using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public BoolData simulate;
    public BoolData collision;
    public BoolData wrap;
    public FloatData gravity;
    public FloatData gravitation;
    public FloatData fixedFPS;
    public StringData fpsText;

    float timeAccumulator;
    float fpsAdverage = 0;
    float fps = 0;
    float smoothing = 0.975f;
    float fixedDeltaTime { get { return 1.0f / fixedFPS.value; } }

    static World instance;
    static public World Instance { get { return instance; } }
    public Vector2 Gravity { get { return new Vector2(0, gravity.value); } }

    public List<Body> bodies { get; set; } = new List<Body>();

    private Vector2 size;
    private void Awake()
    {
        instance = this;
        size = Camera.main.ViewportToWorldPoint(Vector2.one);
    }

    void Update()
    {
        if (!simulate.value) return;
        
        float dt = Time.deltaTime;
        timeAccumulator += Time.deltaTime;

        fps = (1.0f / dt);
        fpsAdverage = (fpsAdverage * smoothing) + (fps * (1.0f - smoothing));
        fpsText.value = "FPS: " + fpsAdverage.ToString("F1");

        GravitationalForce.ApplyForce(bodies, gravitation.value);

        while(timeAccumulator >= fixedDeltaTime)
        {
            bodies.ForEach(body => body.Step(fixedDeltaTime));
            bodies.ForEach(body => Intergrator.SemiImplicitEuler(body, fixedDeltaTime));
            bodies.ForEach(body => body.shape.color = Color.magenta);

            if(collision.value == true)
            {
                Collision.CreateContacts(bodies, out List<Contact> contacts);
                contacts.ForEach(contact => { contact.bodyA.shape.color = Color.red; contact.bodyb.shape.color = Color.red; });
                ContactSolver.Resolve(contacts);
            }

            timeAccumulator -= fixedDeltaTime;
        }

        if(wrap.value == true)
        {
            bodies.ForEach(body => body.position = Utilites.Wrap(body.position, -size, size));
        }

        bodies.ForEach(body => body.force = Vector2.zero);
        bodies.ForEach(body => body.acceleration = Vector2.zero);
    }
}
