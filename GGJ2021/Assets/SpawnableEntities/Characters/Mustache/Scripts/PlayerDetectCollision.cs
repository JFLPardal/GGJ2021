using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDetectCollision : DetectObject
{

    private bool can_attach = false;
    private bool can_enter = true;
    private GameObject object_to_attach = null;
    private PlayerHidden player_hidden = null;

    private void Start()
    {
        player_hidden = GetComponent<PlayerHidden>();
    }

    protected override void HandleCollision(Collider2D collider)
    {
        if(collider.tag == "Animal")
        {
            can_attach = collider.GetComponent<AnimalDetectObject>().CanInteractMustache();
            object_to_attach = collider.gameObject;
        }

        else if(collider.tag == "Building")
        {
            can_enter = collider.GetComponent<BuildingDoor>().IsDoorLocked();
        }
    }

    protected override void HandleStoppedColliding(Collider2D collider)
    {
        if (collider.tag == "Animal")
        {
            can_attach = false;
            object_to_attach = null;
        }
        else if (collider.tag == "Building")
        {
            can_enter = false;
        }
    }

    public void Attach(InputAction.CallbackContext context)
    {
        if(can_attach)
        {
            object_to_attach.GetComponent<DisplayMustache>().AttachMustache();
            object_to_attach.GetComponent<DisplayMustache>().SetPlayer(gameObject);
            player_hidden.HideOn(object_to_attach);
        }
        else if(can_enter)
        {
            
        }
    }
}
