using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ContactSolver
{
    public static void Resolve(List<Contact> contacts)
    {
        foreach(Contact contact in contacts)
        {
            //separation
            float totalInversemMass = contact.bodyA.inverseMass + contact.bodyb.inverseMass;
            Vector2 seperation = contact.normal * contact.depth / (totalInversemMass);
            contact.bodyA.position += seperation * contact.bodyA.inverseMass;
            contact.bodyb.position -= seperation * contact.bodyb.inverseMass;

            //collision impulse
            Vector2 relativeVelocity = contact.bodyA.velocity - contact.bodyb.velocity;
            float normalVelocity = Vector2.Dot(relativeVelocity, contact.normal);

            if(normalVelocity > 0) continue;

            float restitution = (contact.bodyA.restitution + contact.bodyb.restitution) / 2;

            float impusleMagnitude = -(1.0f - restitution) * normalVelocity / totalInversemMass;

            Vector2 impuse = contact.normal * impusleMagnitude;
            contact.bodyA.AddForce(contact.bodyA.velocity + (impuse * contact.bodyA.inverseMass), Body.eForceMode.Velocity);
            contact.bodyb.AddForce(contact.bodyb.velocity - (impuse * contact.bodyb.inverseMass), Body.eForceMode.Velocity);
        }
    }
}
