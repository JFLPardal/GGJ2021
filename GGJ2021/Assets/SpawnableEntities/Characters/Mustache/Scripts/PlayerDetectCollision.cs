using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDetectCollision : DetectObject
{
    [SerializeField] private float distance_to_be_arrested = 3f;
    [SerializeField] private float distance_to_owner = 6f;

    private bool can_attach = false;
    private bool can_enter = false;
    private bool can_leave = false;
    private GameObject object_to_attach = null;
    private GameObject house_entered = null;
    private PlayerHidden player_hidden = null;
    private bool is_inside = false;
    private GameOverHUDAnimations game_over_animations = null;

    private float interval_to_enter_house_again_sec = 3.0f;
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
        if(collider.GetComponent<BBDetectCollision>() != null && !game_over_animations.IsGameOver())
        {
            var BBMustacheDistance = Mathf.Abs(Vector2.Distance(collider.gameObject.transform.position, transform.position));
            if (BBMustacheDistance < distance_to_be_arrested)
            {
                game_over_animations.GameOverFromBBCollision();
            }
        }
        if(collider.GetComponent<Mustacheless>() != null && !game_over_animations.IsGameOver())
        {
            var OwnerMustacheDistance = Mathf.Abs(Vector2.Distance(collider.gameObject.transform.position, transform.position));
            if (OwnerMustacheDistance < distance_to_owner)
            {
                game_over_animations.GameOverFromFoundOwnerCollision();
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
            GetComponent<InsideHouseTrigger>().TriggerOpenHouse(house_entered);
            GetComponent<SpriteRenderer>().sortingLayerName = "InsideHouse";
            transform.position = new Vector2(-23, -8);
            is_inside = true;
        }
        
        else if(is_inside && can_leave)
        {
            GetComponent<InsideHouseTrigger>().TriggerLeaveHouse();
            GetComponent<SpriteRenderer>().sortingLayerName = "Player";
            transform.position = house_entered.transform.position + new Vector3(0,-2,0);
            is_inside = false;
            time_left_house = Time.realtimeSinceStartup;
        }
    }
}
