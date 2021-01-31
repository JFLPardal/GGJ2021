using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    [SerializeField] private bool m_IsGameOver = false;
    [SerializeField] private Canvas m_FadeCanvas = null;
    [SerializeField] private GameObject m_HUDToShowAfterFade = null;
    [SerializeField] private GameOverHUDAnimations m_GameOverHUDAnimations = null;
    [SerializeField] private Button m_RestartButton = null;

    [SerializeField][Range(1.5f, 4)] private float m_ScalingFactor = 2f;
    [SerializeField][Range(1.5f, 4)] private float m_ScalingDuration = 1.5f;

    private bool m_FirstGameOver = true;

    const string tag_HudToShowAfterFade = "HUD";
    const string tag_FadeCanvas = "Fade";

    void Update()
    {
        m_IsGameOver = m_GameOverHUDAnimations.IsGameOver();
        if(m_IsGameOver && m_FirstGameOver)
        {
            FadeToBlack();
            m_FirstGameOver = false;
        }
    }

    private void FadeToBlack()
    {
        foreach(var image in m_FadeCanvas.GetComponentsInChildren<Image>())
        {
            if(image.gameObject.CompareTag(tag_FadeCanvas))
            {
                image.color = Color.black;
                if(m_HUDToShowAfterFade.CompareTag(tag_HudToShowAfterFade))
                {
                    Vector3 scalingFactor = new Vector3(m_ScalingFactor, m_ScalingFactor, 1);
                    LeanTween.scale(m_HUDToShowAfterFade, scalingFactor, m_ScalingDuration);
                    m_RestartButton.gameObject.SetActive(true);
                }
                else
                {
                    Debug.LogError("No valid HUD to resize");
                }
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
