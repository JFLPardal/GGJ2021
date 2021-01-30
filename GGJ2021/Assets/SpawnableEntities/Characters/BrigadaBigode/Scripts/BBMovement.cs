using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBMovement : NPCMovement
{
    private Vector2 followTo = Vector2.zero;
    private float min_distance_to_catch = 2.0f;
    private float follow_speed = 5.0f;
    private int stopped_colliding_interval_mins = 2;
    private float initial_time = 0.0f;
    private bool is_seeing_object = false;

    public void StartFollowing(Vector2 position)
    {
        followTo = position;
        SetObjectIsInSight();
    }

    public void StopFollowing()
    {
        is_seeing_object = false;
    }

    protected override void HandleMovement()
    {
        if(!IsCloseTo())
            Move();
    }

    protected override void Move()
    {
        if (IsFollowing())
        {
            Follow();
        }
        else
        {
            base.Move();
        }
    }

    private void Follow()
    {
        Vector2 velocity = followTo.normalized * follow_speed;
        m_rb.MovePosition(m_rb.position + velocity * Time.deltaTime);
    }

    private bool IsFollowing()
    {
        var keepFollowing = !is_seeing_object && (Time.realtimeSinceStartup - initial_time >= stopped_colliding_interval_mins);
        if(!is_seeing_object && Time.realtimeSinceStartup - initial_time >= stopped_colliding_interval_mins)
        {
            initial_time = 0.0f;
            return false;
        }
        return followTo != Vector2.zero;
    }

    private bool IsCloseTo()
    {
        return IsFollowing() && Vector2.Distance(m_rb.position, followTo) <= min_distance_to_catch;
    }

    private void SetObjectIsInSight()
    {
        if (!is_seeing_object)
            initial_time = Time.realtimeSinceStartup;

        is_seeing_object = true;        
    }

}
