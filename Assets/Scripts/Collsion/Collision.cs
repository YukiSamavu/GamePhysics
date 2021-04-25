using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision
{
    public static void CreateContacts(List<Body> bodies, out List<Contact> contacts)
    {
        contacts = new List<Contact>();

        Circle circleA = new Circle(Vector2.zero, 1);
        Circle circleB = new Circle(Vector2.zero, 1);

        for (int i = 0; i < bodies.Count -1; i++)
        {
            for(int j = i + 1; j < bodies.Count; j++)
            {
                Body bodyA = bodies[i];
                Body bodyB = bodies[j];

                if (bodyA.type == Body.eType.Static && bodyB.type == Body.eType.Static) continue;

                circleA.center = bodyA.position;
                circleA.radius = ((CircleShape)bodyA.shape).radius;

                circleB.center = bodyB.position;
                circleB.radius = ((CircleShape)bodyB.shape).radius;

                //Circle circleA = new Circle(bodyA.position, ((CircleShape)bodyA.shape).radius);
                //Circle circleB = new Circle(bodyB.position, ((CircleShape)bodyB.shape).radius);

                if (circleA.Contains(circleB))
                {
                    Contact contact = new Contact() { bodyA = bodyA, bodyb = bodyB };
                    Vector2 direction = circleA.center - circleB.center;
                    float distance = direction.magnitude;
                    contact.depth = (circleA.radius + circleB.radius) - distance;
                    contact.normal = direction.normalized;
                    contacts.Add(contact);
                }
            }
        }
    }
}
