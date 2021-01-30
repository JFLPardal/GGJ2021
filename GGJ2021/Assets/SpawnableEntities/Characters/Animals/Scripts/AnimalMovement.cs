using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMovement : NPCMovement
{

    protected override void UpdateSpriteFacingDirection()
    {

        if (IsGoingLeft())
        {
            Debug.Log("left");
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (IsGoingRight())
        {
            Debug.Log("right");
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
