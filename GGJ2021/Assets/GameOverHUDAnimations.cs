using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverHUDAnimations : MonoBehaviour
{
    [SerializeField] private bool m_FoundOwner = false;
    [SerializeField] private bool m_Arrested = false;

    const string animator_trigger_foundOwner = "FoundOwner";
    const string animator_trigger_arrested = "Arrested";

    private Animator m_Animator = null;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(m_FoundOwner)
        {
            m_Animator.SetTrigger(animator_trigger_foundOwner);
            m_FoundOwner = false;
        }
        else if(m_Arrested)
        {
            m_Animator.SetTrigger(animator_trigger_arrested);
            m_Arrested = false;
        }
    }
}
