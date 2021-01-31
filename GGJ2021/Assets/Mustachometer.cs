using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mustachometer : MonoBehaviour
{
    [SerializeField] private Transform m_MustacheMovement;
    [SerializeField] private Animator m_LeftAnimator = null;
    [SerializeField] private Animator m_RightAnimator = null;
    private Transform m_OwnerPosition;


    private const string animator_float_mustacheDistanceToOwner = "mustacheDistanceToOwner";

    void Update()
    {
        if (m_OwnerPosition != null)
        {
            float distanceFromMustacheToOwner = Vector2.Distance(m_MustacheMovement.position, m_OwnerPosition.position);
            m_LeftAnimator.SetFloat(animator_float_mustacheDistanceToOwner, distanceFromMustacheToOwner);
            m_RightAnimator.SetFloat(animator_float_mustacheDistanceToOwner, distanceFromMustacheToOwner);
        }
    }

    public void SetOwnerPosition(Transform position)
    {
        m_OwnerPosition = position;
    }
}
