using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class BBMovement : NPCMovement
{
    private Vector2 followTo = Vector2.zero;
    [SerializeField] private float follow_speed = 3.5f; 
    private bool triggerStopFollowing = false;
    private bool triggerFollow = false;
    private float initial_time_stopped_following = 0.0f;
    private float initial_time_stopped_colliding = 0.0f;
    private bool is_seeing_object = false;
    private bool stop_moving = false;

    public bool found = false;
    [SerializeField]
    private AudioSource source;

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
        if (!found)
        {
            if (IsFollowing() && !IsCloseTo())
            {
                Follow();
                if (triggerFollow)
                {
                    GetComponent<BBEventSystem>().TriggerBBFollowing();
                    triggerFollow = false;
                }
                triggerStopFollowing = true;
            }
            else if (!StoppedFollowingCooldown())
            {
                base.Move();
                if (triggerStopFollowing)
                {
                    GetComponent<BBEventSystem>().TriggerBBStoppedFollowing();
                    triggerStopFollowing = false;
                }
                triggerFollow = true;
            }
        }
    }

    protected override void UpdateAnimator()
    {
        if(StoppedFollowingCooldown())
            m_Animator.SetBool(Constants.animator_bool_isWalking, false);
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

        var keepFollowing = Time.realtimeSinceStartup - initial_time_stopped_colliding < Constants.bb_stopped_colliding_interval_sec;

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
        var remainStopped = Time.realtimeSinceStartup - initial_time_stopped_following < Constants.bb_stopped_following_interval_sec;
        if (!remainStopped) stop_moving = false;
        return remainStopped;
    }

    private bool IsCloseTo()
    {
        return Vector2.Distance(m_rb.position, followTo) <= Constants.min_dist_bb_to_catch;
    }

    private void SetObjectIsInSight()
    {
        if (!is_seeing_object)
            initial_time_stopped_colliding = Time.realtimeSinceStartup;

        is_seeing_object = true;        
    }

}
