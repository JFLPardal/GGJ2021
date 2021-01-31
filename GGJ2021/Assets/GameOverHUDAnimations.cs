using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverHUDAnimations : MonoBehaviour
{
    [SerializeField] private bool m_FoundOwner = false;
    [SerializeField] private bool m_Arrested = false;
    [SerializeField] private bool m_GameIsOver = false;

    private AudioSource source;
    [SerializeField] private AudioClip win;
    [SerializeField] private AudioClip lose;

    const string animator_trigger_foundOwner = "FoundOwner";
    const string animator_trigger_arrested = "Arrested";

    private Animator m_Animator = null;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(m_FoundOwner)
        {
            m_Animator.SetTrigger(animator_trigger_foundOwner);
            m_FoundOwner = false;
            source.clip = win;
            source.Play();
        }
        else if(m_Arrested)
        {
            m_Animator.SetTrigger(animator_trigger_arrested);
            m_Arrested = false;
            source.clip = lose;
            source.Play();
        }
    }
    public void GameOverFromBBCollision()
    {
        m_Arrested = true;
        m_GameIsOver = true;
    }
    public void GameOverFromFoundOwnerCollision()
    {
        m_FoundOwner = true;
        m_GameIsOver = true;
    }

    public bool IsGameOver() { return m_GameIsOver; }
}
