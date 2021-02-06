using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class PlayerInitlaSetup : MonoBehaviour
{
    private GameObject droplets;
    private Animator m_Animator = null;
    private float init_is_wet = 0.0f;
    private bool can_move = false;
    [SerializeField]
    private AudioSource sourceDrop;

    [SerializeField]
    private AudioSource sourceSplash;
    // Start is called before the first frame update
    void Start()
    {
        init_is_wet = Time.realtimeSinceStartup;
        m_Animator = GetComponent<Animator>();
        m_Animator.SetBool(Constants.animator_bool_player_wet, true);
        droplets = transform.GetChild(0).gameObject;
        droplets.GetComponent<Animator>().SetBool(Constants.animator_bool_player_wet, true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.realtimeSinceStartup - init_is_wet >= Constants.interval_is_wet)
        {
            m_Animator.SetBool(Constants.animator_bool_player_wet, false);
            droplets.GetComponent<Animator>().SetBool(Constants.animator_bool_player_wet, false);
            can_move = true;
            StopDropletsSounds();
        }
    }

    private void StopDropletsSounds()
    {
        sourceDrop.Stop();
        sourceDrop.loop = false;
        sourceSplash.Stop();
        sourceSplash.loop = false;
    }

    public bool CanStartMoving()
    {
        return can_move;
    }

    public void HideDroplets()
    {
        droplets.SetActive(false);
    }
}
