using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class BuildingDetectObject : DetectObject
{
    private Animator m_Animator = null;
    private BuildingDoor buildingDoorClass;
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        buildingDoorClass = GetComponent<BuildingDoor>();
    }

    protected override void HandleCollision(Collider2D collider)
    {
        if(collider.tag == Constants.player_tag)
        {
            m_Animator.SetBool(Constants.animator_bool_is_open, !buildingDoorClass.IsDoorLocked());
        }
    }

    protected override void HandleStoppedColliding(Collider2D collider)
    {
        if (collider.tag == Constants.player_tag)
        {
            m_Animator.SetBool(Constants.animator_bool_is_open, false);
        }
    }
}
