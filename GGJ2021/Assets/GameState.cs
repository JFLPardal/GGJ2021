using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Utils;

namespace GameControl
{
    public enum State
    {
        MainMenu,
        InGame,
        InMenu,
        GameOver
    }

    public class GameState : MonoBehaviour
    {
        private static bool m_IsGameOver = false;
        [SerializeField] private Canvas m_FadeCanvas = null;
        [SerializeField] private GameObject m_HUDToShowAfterFade = null;
        [SerializeField] private GameOverHUDAnimations m_GameOverHUDAnimations = null;
        [SerializeField] private Button m_RestartButton = null;

        [SerializeField] [Range(1.5f, 4)] private float m_ScalingFactor = 2f;
        [SerializeField] [Range(1.5f, 4)] private float m_ScalingDuration = 1.5f;

        private static State game_state = State.MainMenu;
        private static bool m_FirstGameOver = true;
        private static bool changed_scene = false;
        private AudioSource source;
        private GenerateOwner generateOwnerClass;

        private void Start()
        {
            source = GetComponent<AudioSource>();
            generateOwnerClass = GetComponent<GenerateOwner>();
        }

        void Update()
        {
            if (changed_scene && SceneManager.GetActiveScene().buildIndex == 1)
            {
                Debug.Log("reload");
                SetNecessaryObjects();
                generateOwnerClass.OnStartGame();
                changed_scene = false;
            }

            if (game_state == State.InGame && !changed_scene)
            {
                //Debug.Log("handle in game");
                HandleInGameLoop();
                if(m_FadeCanvas == null) //need to check this, make sure everything has the proper game object
                {
                    SetNecessaryObjects();
                    generateOwnerClass.OnStartGame();
                }
                //Debug.Log(m_FadeCanvas == null);
                //Debug.Log(m_HUDToShowAfterFade == null);
                //Debug.Log(m_RestartButton == null);
                //Debug.Log(m_GameOverHUDAnimations == null);
            }

        }

        private void SetNecessaryObjects()
        {
            m_FadeCanvas = (Canvas)FindObjectOfType(typeof(Canvas));
            m_HUDToShowAfterFade = GameObject.FindGameObjectWithTag(Constants.hud_tag);
            m_RestartButton = (Button)FindObjectOfType(typeof(Button));
            m_GameOverHUDAnimations = (GameOverHUDAnimations)FindObjectOfType(typeof(GameOverHUDAnimations));
            m_RestartButton.gameObject.SetActive(false);
        }

        private void HandleInGameLoop()
        {
            m_IsGameOver = m_GameOverHUDAnimations.IsGameOver();
            if (m_IsGameOver && m_FirstGameOver)
            {
                FadeToBlack();
                m_FirstGameOver = false;
                game_state = State.GameOver;
                generateOwnerClass.OnGameOver();
            }
        }

        private void FadeToBlack()
        {
            foreach (var image in m_FadeCanvas.GetComponentsInChildren<Image>())
            {
                source.Stop();
                if (image.gameObject.CompareTag(Constants.tag_FadeCanvas))
                {
                    image.color = Color.black;
                    if (m_HUDToShowAfterFade.CompareTag(Constants.tag_HudToShowAfterFade))
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
            changed_scene = true;
            game_state = State.InGame;
            Debug.Log("restart");
            m_IsGameOver = false;
            m_FirstGameOver = true;
        }

        public void StartGame()
        {
            SceneManager.LoadScene("Showcase");
            game_state = State.InGame;
            changed_scene = true;
        }
    }
}