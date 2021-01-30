using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBMovement : NPCMovement
{
    private Vector2 followTo = Vector2.zero;
    private float min_distance_to_catch = 2.0f;
    private float follow_speed = 3.5f;
    private int stopped_colliding_interval_sec = 2;
    private int stopped_following_interval_sec = 2;
    private float initial_time_stopped_following = 0.0f;
    private float initial_time_stopped_colliding = 0.0f;
    private bool is_seeing_object = false;
    private bool stop_moving = false;

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
        if (IsFollowing() && !IsCloseTo())
        {
            Follow();
        }
        else if(!StoppedFollowingCooldown())
        {
            base.Move();
        }

        if(IsFollowing() && IsCloseTo())
        {
            //trigger game over event
        }
    }

    protected override void UpdateAnimator()
    {
        if(StoppedFollowingCooldown())
            m_Animator.SetBool(animator_bool_isWalking, false);
        else
            base.UpdateAnimator();
    }

    private void Follow()
    {
        direction = (followTo - m_rb.position).normalized;
        Vector2 velocity = direction * follow_speed;
        m_rb.MovePosition(m_rb.position + velocity * Time.deltaTime);
    }

    private bool IsFollowing()
    {
        if (followTo == Vector2.zero)
            return false;

        if (is_seeing_object)
            return true;

        var keepFollowing = Time.realtimeSinceStartup - initial_time_stopped_colliding < stopped_colliding_interval_sec;

        if(!keepFollowing)
        {
            OnStopFollowing();
        }

        return keepFollowing;
    }

    private void OnStopFollowing()
    {
        initial_time_stopped_colliding = 0.0f;
        followTo = Vector2.zero;

        if (!stop_moving)
            initial_time_stopped_following = Time.realtimeSinceStartup;

        stop_moving = true;
    }

    private bool StoppedFollowingCooldown()
    {
        var remainStopped = Time.realtimeSinceStartup - initial_time_stopped_following < stopped_following_interval_sec;
        if (!remainStopped) stop_moving = false;
        return remainStopped;
    }

    private bool IsCloseTo()
    {
        return Vector2.Distance(m_rb.position, followTo) <= min_distance_to_catch;
    }

    private void SetObjectIsInSight()
    {
        if (!is_seeing_object)
            initial_time_stopped_colliding = Time.realtimeSinceStartup;

        is_seeing_object = true;        
    }

}
