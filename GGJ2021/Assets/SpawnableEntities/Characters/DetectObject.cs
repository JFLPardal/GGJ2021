using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public abstract class DetectObject : MonoBehaviour
{
    private CircleCollider2D circle_collider = null;

    // Start is called before the first frame update
    void Start()
    {
        circle_collider = GetComponent<CircleCollider2D>();
        circle_collider.radius = 4;
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
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
