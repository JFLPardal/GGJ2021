using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public abstract class DetectObject : MonoBehaviour
{
    protected CircleCollider2D circle_collider = null;
    
    private void OnTriggerStay2D(Collider2D collider)
    {
        HandleCollision(collider);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        HandleStoppedColliding(collider);
    }

    protected abstract void HandleCollision(Collider2D collider);

    protected abstract void HandleStoppedColliding(Collider2D collider);
}
