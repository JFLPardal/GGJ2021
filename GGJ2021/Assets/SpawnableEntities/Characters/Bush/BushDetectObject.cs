using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class BushDetectObject : DetectObject
{
    private AttachableBehaviour attachableBehaviour;

    private void Start()
    {
        attachableBehaviour = GetComponent<AttachableBehaviour>();
    }

    protected override void HandleCollision(Collider2D collider)
    {
        if (collider.tag == Constants.player_tag)
        {
            attachableBehaviour.SetCanInteract(true);
            attachableBehaviour.HandleCanInteractIndicator();
        }
    }

    protected override void HandleStoppedColliding(Collider2D collider)
    {
        if (collider.tag == Constants.player_tag)
        {
            attachableBehaviour.SetCanInteract(false);
            attachableBehaviour.HandleCanInteractIndicator();
        }
    }
}
