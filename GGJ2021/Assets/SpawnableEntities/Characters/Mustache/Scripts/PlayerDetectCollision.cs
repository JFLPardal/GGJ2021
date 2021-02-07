using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

public class PlayerDetectCollision : DetectObject
{
    [SerializeField] private float distance_to_be_arrested = 3f;
    [SerializeField] private float distance_to_owner = 6f;

    private bool can_attach = false;
    private bool can_enter = false;
    private bool can_leave = false;
    private GameObject object_to_attach = null;
    private GameObject building_entered = null;
    private PlayerHidden player_hidden = null;
    private bool is_inside = false;
    private GameOverHUDAnimations game_over_animations = null;

    private float time_left_house = 0.0f;

    private void Start()
    {
        player_hidden = GetComponent<PlayerHidden>();

        game_over_animations = FindObjectOfType<GameOverHUDAnimations>();
        if (game_over_animations == null)
        {
            Debug.LogError("no GameOverHudAnimations reference");
        }
    }

    private void HandleCollisionWithAttachable(Collider2D collider)
    {
        can_attach = collider.GetComponent<AttachableBehaviour>().CanInteract();
        object_to_attach = collider.gameObject;
    }

    private void HandleCollisionWithBuilding(Collider2D collider)
    {
        if (Time.realtimeSinceStartup - time_left_house > Constants.interval_to_enter_house_again_sec && !is_inside)
        {
            can_enter = !collider.GetComponent<BuildingDoor>().IsDoorLocked();
            building_entered = collider.gameObject;
        }
    }

    private void HandleCollisionWithBB(Collider2D collider)
    {
        var BBMustacheDistance = Mathf.Abs(Vector2.Distance(collider.gameObject.transform.position, transform.position));
        if (BBMustacheDistance < distance_to_be_arrested)
        {
            game_over_animations.GameOverFromBBCollision();
            collider.GetComponent<BBMovement>().found = true;
        }
    }

    private void HandleCollisionWithOwner(Collider2D collider)
    {
        var OwnerMustacheDistance = Mathf.Abs(Vector2.Distance(collider.gameObject.transform.position, transform.position));
        if (OwnerMustacheDistance < distance_to_owner)
        {
            game_over_animations.GameOverFromFoundOwnerCollision();
        }
    }

    protected override void HandleCollision(Collider2D collider)
    {
        if (collider.tag == Constants.inside_building_tag)
        {
            can_leave = true;
        }
        else if (collider.tag == Constants.animal_tag || collider.tag == Constants.attachable_tag)
        {
            HandleCollisionWithAttachable(collider);
        }
        else if(collider.tag == Constants.building_tag)
        {
            HandleCollisionWithBuilding(collider);
        }

        if(collider.GetComponent<BBDetectCollision>() != null && !game_over_animations.IsGameOver())
        {
            HandleCollisionWithBB(collider);
        }
        if(collider.GetComponent<Mustacheless>() != null && !game_over_animations.IsGameOver())
        {
            HandleCollisionWithOwner(collider);
        }
    }

    protected override void HandleStoppedColliding(Collider2D collider)
    {
        if (collider.tag == Constants.inside_building_tag)
        {
            can_leave = false;
        }
        else if (collider.tag == Constants.animal_tag || collider.tag == Constants.attachable_tag)
        {
            can_attach = false;
            object_to_attach = null;
        }
        else if (collider.tag == Constants.building_tag)
        {
            can_enter = false;
            if(!is_inside)
                building_entered = null;
        }
    }

    public void Attach(InputAction.CallbackContext context)
    {
        if(can_attach)
        {
            AttachToObjectOrAnimal();
        }
        else if(can_enter && !is_inside)
        {
            EnterBuilding();
        }        
        else if(is_inside && can_leave)
        {
            LeaveBuilding();
        }
    }

    private void AttachToObjectOrAnimal()
    {
        object_to_attach.GetComponent<DisplayMustache>().AttachMustache();
        object_to_attach.GetComponent<DisplayMustache>().SetPlayer(gameObject);
        player_hidden.HideOn(object_to_attach);
    }

    private void EnterBuilding()
    {
        GetComponent<InsideHouseTrigger>().TriggerOpenHouse(building_entered);
        GetComponent<SpriteRenderer>().sortingLayerName = "InsideHouse";
        transform.position = new Vector2(-23, -8);
        is_inside = true;
    }

    private void LeaveBuilding()
    {
        GetComponent<InsideHouseTrigger>().TriggerLeaveHouse();
        GetComponent<SpriteRenderer>().sortingLayerName = "Player";
        transform.position = building_entered.transform.position + new Vector3(0, -15, 0);
        is_inside = false;
        time_left_house = Time.realtimeSinceStartup;
    }
}
