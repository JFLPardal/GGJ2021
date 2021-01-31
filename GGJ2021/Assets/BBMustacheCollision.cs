using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBMustacheCollision : MonoBehaviour
{
    private CircleCollider2D m_triggerArea = null;
    private GameOverHUDAnimations m_GameOverAnimations = null;
    void Start()
    {
        m_triggerArea = GetComponent<CircleCollider2D>();
        m_GameOverAnimations = FindObjectOfType<GameOverHUDAnimations>();
        if(m_GameOverAnimations == null)
        {
            Debug.LogError("no GameOverHudAnimations reference");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<BBDetectCollision>() != null)
        {
            m_GameOverAnimations.GameOverFromBBCollision();
        }
    }
}
