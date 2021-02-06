using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class BBDetectCollision : DetectObject
{

    private BBMovement movementClass = null;
    private Rigidbody2D m_rb = null;
    [SerializeField] private float max_angle = 45.0f;
    private bool can_follow_player = true;

    private void Start()
    {
        movementClass = GetComponent<BBMovement>();
        m_rb = GetComponent<Rigidbody2D>();
        circle_collider = GetComponent<CircleCollider2D>();
        circle_collider.radius = Constants.bb_collision_circle_radius;
    }

    protected override void HandleCollision(Collider2D collider)
    {
        var closestPointOfContact = collider.ClosestPoint(m_rb.position);
        var direction = closestPointOfContact - m_rb.position;
        Vector2 normal = direction.normalized;

        if (Vector2.Angle(movementClass.direction, normal) <= max_angle)
        {
            if (collider.tag == "Player" && can_follow_player)
            {
                movementClass.StartFollowing(collider.transform.position);
            }
        }
    }

    protected override void HandleStoppedColliding(Collider2D collider)
    {
        movementClass.StopFollowing();
    }

    private void OnDrawGizmos()
    {
        if (movementClass != null)
        {
            float angle = 45.0f;
            float rayRange = 5.0f;
            float halfFOV = angle / 2.0f;

            Quaternion upRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.forward);
            Quaternion downRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.forward);

            Vector3 upRayDirection = upRayRotation * movementClass.direction * rayRange;
            Vector3 downRayDirection = downRayRotation * movementClass.direction * rayRange;

            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, upRayDirection);
            Gizmos.DrawRay(transform.position, downRayDirection);
        }
    }

    public void CanFollowPlayer(bool follow)
    {
        can_follow_player = follow;
    }
}
