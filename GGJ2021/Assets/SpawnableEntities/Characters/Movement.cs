using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] [Range(5, 20)] float MoveSpeed = 10;

    private const string animator_bool_isWalking = "isWalking";
    private Rigidbody2D m_rb = null;
    private SpriteRenderer m_SpriteRenderer = null;
    private Animator m_Animator = null;

    private Vector2 velocity = Vector2.zero;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        m_rb.velocity = velocity;

        UpdateAnimator();
        UpdateFacingDirection();
    }

    private void UpdateAnimator()
    {
        bool isWalking = (velocity.x != 0 || velocity.y != 0) ? true : false;
        m_Animator.SetBool(animator_bool_isWalking, isWalking);
    }

    private void UpdateFacingDirection()
    {
        if (velocity.x != 0)
        {
            if (velocity.x > 0)
            {
                m_SpriteRenderer.flipX = true;
            }
            else if (velocity.x < 0)
            {
                m_SpriteRenderer.flipX = false;
            }
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        velocity = context.ReadValue<Vector2>() * MoveSpeed;
    }
}
