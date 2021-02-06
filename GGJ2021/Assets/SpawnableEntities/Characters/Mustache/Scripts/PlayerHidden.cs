using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHidden : MonoBehaviour
{

    private Movement movement = null;
    private SpriteRenderer spriteRenderer = null;
    private CircleCollider2D collider = null;
    private Rigidbody2D rb = null;
    private bool is_hidden = false;
    private Vector2 position_to_reveal = Vector2.zero;
    private GameObject objectHidOn = null;

    private bool can_reveal = false;


    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<Movement>();
        collider = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void AllowReveal(Vector2 position)
    {
        can_reveal = true;
        position_to_reveal = position;
    }

    public void DisallowReveal()
    {
        can_reveal = false;
    }

    public void HideOn(GameObject obj)
    {
        rb.velocity = Vector2.zero;
        movement.enabled = false;
        collider.enabled = false;
        spriteRenderer.enabled = false;
        is_hidden = true;
        objectHidOn = obj;
    }

    private void Show()
    {
        if (is_hidden)
        {
            movement.enabled = true;
            collider.enabled = true;
            spriteRenderer.enabled = true;
            transform.position = position_to_reveal;
            is_hidden = true;
        }
    }

    public void Reveal(InputAction.CallbackContext context)
    {
        if (is_hidden && can_reveal)
        {
            Show();
            objectHidOn.GetComponent<DisplayMustache>().DettachMustache();
        }
    }
}
