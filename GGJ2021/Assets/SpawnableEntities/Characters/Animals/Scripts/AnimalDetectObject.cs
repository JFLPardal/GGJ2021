using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalDetectObject : DetectObject
{

    private DisplayMustache displayMustache;
    private bool can_attach_detach = false;
    private GameObject player;

    private void Start()
    {
        displayMustache = GetComponent<DisplayMustache>();
    }

    protected override void HandleCollision(Collider2D collider)
    {
        if (collider.tag == "ControlPoint")
        {
            if (displayMustache.HasMustache())
            {
                displayMustache.AllowPlayerToBeRevealed();
            }
            can_attach_detach = true;
        }
            
    }

    protected override void HandleStoppedColliding(Collider2D collider)
    {
        if (collider.tag == "ControlPoint")
        {
            if (displayMustache.HasMustache())
                displayMustache.DontAllowPlayerToBeRevealed();
            can_attach_detach = false;
        }
        
    }

    public bool CanInteractMustache()
    {
        Debug.Log("can_attach_mustache: " + can_attach_detach);
        return can_attach_detach;
    }
}
