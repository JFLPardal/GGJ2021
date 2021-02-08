using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class AnimalDetectObject : DetectObject
{

    private DisplayMustache displayMustache;
    private AttachableBehaviour attachableBehaviour;
    private bool can_attach_detach = false;

    private void Start()
    {
        displayMustache = GetComponent<DisplayMustache>();
        attachableBehaviour = GetComponent<AttachableBehaviour>();
    }

    protected override void HandleCollision(Collider2D collider)
    {
        if (collider.tag == Constants.control_point_tag)
        {
            if (displayMustache.HasMustache())
            {
                displayMustache.AllowPlayerToBeRevealed();
                attachableBehaviour.SetCanInteract(true);
                attachableBehaviour.HandleCanInteractIndicator();
            }
            else
                attachableBehaviour.SetCanInteract(true);
        }
        
        if(collider.tag == Constants.player_tag)
        {
            attachableBehaviour.HandleCanInteractIndicator();
        }
    }

    protected override void HandleStoppedColliding(Collider2D collider)
    {
        if (collider.tag == Constants.control_point_tag)
        {
            if (displayMustache.HasMustache())
                displayMustache.DontAllowPlayerToBeRevealed();

            attachableBehaviour.SetCanInteract(false);
            attachableBehaviour.HandleCanInteractIndicator();
        }

        if (collider.tag == Constants.player_tag)
        {
            attachableBehaviour.HandleCanInteractIndicator();
        }
    }
}
