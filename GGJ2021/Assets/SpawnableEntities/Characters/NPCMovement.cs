using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 2.0f;

    protected string animator_bool_isWalking = "isWalking";
    protected Rigidbody2D m_rb = null;
    protected SpriteRenderer m_SpriteRenderer = null;
    protected Animator m_Animator = null;

    public Vector2 direction = Vector2.right;

    [SerializeField]
    private List<GameObject> Control_Points;

    private GameObject current_control_point = null;
    private int current_control_point_id = 0;
    private bool move_forward = true;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Animator = GetComponent<Animator>();
        current_control_point = Control_Points[current_control_point_id];
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimator();
        UpdateSpriteFacingDirection();
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
        if(Vector2.Distance(current_control_point.transform.position, m_rb.position) > 0.5f) {
            var control_point_v2 = new Vector2(current_control_point.transform.position.x, current_control_point.transform.position.y);
            direction = (control_point_v2 - m_rb.position).normalized;
            Vector2 velocity = direction * speed;
            m_rb.MovePosition(m_rb.position + velocity * Time.deltaTime);
        }
        else
        {
            OnReachedControlPoint();
        }
    }

    protected virtual void OnReachedControlPoint()
    {
        UpdateNextControlPoint();
    }

    private void UpdateNextControlPoint()
    {
        if (move_forward)
        {
            ++current_control_point_id;
            if (ReachedEndOfControlPoints())
            {
                OnReachedEndOfControlPoints();
            }
        }
        else
        {
            --current_control_point_id;
            if (ReachedBeginningOfControlPoints())
            {
                OnReachedBeginningOfControlPoints();
            }
        }

        current_control_point = Control_Points[current_control_point_id];
    }

    private bool ReachedEndOfControlPoints()
    {
        return current_control_point_id >= Control_Points.Count;
    }

    private bool ReachedBeginningOfControlPoints()
    {
        return current_control_point_id < 0;
    }

    protected bool IsGoingRight()
    {
        return direction.x >= 0.0f && direction.x <= 1.0f;
    }

    protected bool IsGoingLeft()
    {
        return direction.x >= -1.0f && direction.x < 0.0f;
    }

    protected virtual void UpdateAnimator()
    {
        if(m_Animator != null)
            m_Animator.SetBool(animator_bool_isWalking, true);
    }

    protected virtual void UpdateSpriteFacingDirection()
    {
        if (IsGoingRight())
        {
            m_SpriteRenderer.flipX = false;
        }
        else if(IsGoingLeft())
        {
            m_SpriteRenderer.flipX = true;
        }
    }

    protected virtual void OnReachedEndOfControlPoints()
    {
        move_forward = false;
        current_control_point_id = Control_Points.Count - 2;
    }

    protected virtual void OnReachedBeginningOfControlPoints()
    {
        move_forward = true;
        current_control_point_id = 1;
    }
}
