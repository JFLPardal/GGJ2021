using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBDetectCollision : DetectObject
{

    private BBMovement movementClass = null;
    private Rigidbody2D m_rb = null;
    private float max_angle = 45.0f;

    private void Start()
    {
        movementClass = GetComponent<BBMovement>();
        m_rb = GetComponent<Rigidbody2D>();
    }

    protected override void HandleCollision(Collider2D collider)
    {
        var closestPointOfContact = collider.ClosestPoint(m_rb.position);
        var direction = closestPointOfContact - m_rb.position;
        Vector2 normal = direction.normalized;
        Vector2 vel = m_rb.velocity;

        if (Vector2.Angle(vel, -normal) <= max_angle)
        {
            if (collider.tag == "Player")
            {
                movementClass.StartFollowing(collider.transform.position);
            }
        }
    }

    protected override void HandleStoppedColliding(Collider2D collider)
    {
        movementClass.StopFollowing();
    }

}
