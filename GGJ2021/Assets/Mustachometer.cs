using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mustachometer : MonoBehaviour
{
    [SerializeField] private Transform m_MustacheMovement;
    [SerializeField] private Transform m_OwnerPosition;
    [SerializeField] private Animator m_LeftAnimator = null;
    [SerializeField] private Animator m_RightAnimator = null;


    private const string animator_float_mustacheDistanceToOwner = "mustacheDistanceToOwner";

    void Update()
    {
        float distanceFromMustacheToOwner = Vector2.Distance(m_MustacheMovement.position, m_OwnerPosition.position);
        m_LeftAnimator.SetFloat(animator_float_mustacheDistanceToOwner, distanceFromMustacheToOwner);
        m_RightAnimator.SetFloat(animator_float_mustacheDistanceToOwner, distanceFromMustacheToOwner);
    }
}
