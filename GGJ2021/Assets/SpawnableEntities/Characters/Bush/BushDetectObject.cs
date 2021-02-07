using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class BushDetectObject : DetectObject
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
        if (collider.tag == Constants.player_tag)
        {
            attachableBehaviour.SetCanInteract(true);
            displayMustache.AllowPlayerToBeRevealed();
        }
    }

    protected override void HandleStoppedColliding(Collider2D collider)
    {
        
    }
}
