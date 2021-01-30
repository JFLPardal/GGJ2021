using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCMovement : MonoBehaviour
{

    private float speed = 2.0f;

    private string animator_bool_isWalking = "isWalking";
    protected Rigidbody2D m_rb = null;
    protected SpriteRenderer m_SpriteRenderer = null;
    protected Animator m_Animator = null;

    private Vector2 direction = Vector2.right;
    private Vector2 starting_position = Vector2.zero;
    private float distance = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Animator = GetComponent<Animator>();
        starting_position = m_rb.position;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimator();
        UpdateSpriteFacingDirection();
        UpdateMovementDirection();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    protected virtual void HandleMovement()
    {
        Move();
    }

    protected virtual void Move()
    {
        Vector2 velocity = direction * speed;
        m_rb.MovePosition(m_rb.position + velocity * Time.deltaTime);
    }

    protected virtual void UpdateAnimator()
    {
        m_Animator.SetBool(animator_bool_isWalking, true);
    }

    protected virtual void UpdateSpriteFacingDirection()
    {
        if (Vector2.Distance(starting_position, m_rb.position) > distance)
        {
            m_SpriteRenderer.flipX = !m_SpriteRenderer.flipX;
        }
    }

    protected virtual void UpdateMovementDirection()
    {
        if (m_SpriteRenderer.flipX)
            direction = Vector2.left;
        else
            direction = Vector2.right;
    }
}
