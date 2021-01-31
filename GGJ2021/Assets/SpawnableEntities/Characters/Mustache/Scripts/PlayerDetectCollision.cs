using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDetectCollision : DetectObject
{

    private bool can_attach = false;
    private bool can_enter = true;
    private bool can_leave = false;
    private GameObject object_to_attach = null;
    private GameObject house_entered = null;
    private PlayerHidden player_hidden = null;
    private bool is_inside = false;

    private float interval_to_enter_house_again_sec = 3.0f;
    private float time_left_house = 0.0f;

    private void Start()
    {
        player_hidden = GetComponent<PlayerHidden>();
    }

    protected override void HandleCollision(Collider2D collider)
    {
        Debug.Log(collider.tag);
        if (collider.tag == "InsideBuilding")
        {
            can_leave = true;
        }
        else if (collider.tag == "Animal")
        {
            can_attach = collider.GetComponent<AnimalDetectObject>().CanInteractMustache();
            object_to_attach = collider.gameObject;
        }

        else if(collider.tag == "Building")
        {
            if (Time.realtimeSinceStartup - time_left_house > interval_to_enter_house_again_sec && !is_inside)
            {
                can_enter = !collider.GetComponent<BuildingDoor>().IsDoorLocked();
                house_entered = collider.gameObject;
            }
        }
    }

    protected override void HandleStoppedColliding(Collider2D collider)
    {
        if (collider.tag == "InsideBuilding")
        {
            can_leave = false;
        }
        else if (collider.tag == "Animal")
        {
            can_attach = false;
            object_to_attach = null;
        }
        else if (collider.tag == "Building")
        {
            can_enter = false;
            if(!is_inside)
                house_entered = null;
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
        else if(can_enter && !is_inside)
        {
            GetComponent<InsideHouseTrigger>().TriggerOpenHouse();
            GetComponent<SpriteRenderer>().sortingLayerName = "InsideHouse";
            transform.position = new Vector2(-21,-25);
            is_inside = true;
        }
        
        else if(is_inside && can_leave)
        {
            GetComponent<InsideHouseTrigger>().TriggerLeaveHouse();
            GetComponent<SpriteRenderer>().sortingLayerName = "Player";
            Debug.Log(house_entered.name);
            transform.position = house_entered.transform.position + new Vector3(0,-2,0);
            is_inside = false;
            time_left_house = Time.realtimeSinceStartup;
        }
    }
}
