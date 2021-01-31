using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInitlaSetup : MonoBehaviour
{
    private GameObject droplets;
    private Animator m_Animator = null;
    private float interval_is_wet = 5.0f;
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
        m_Animator.SetBool("isWet", true);
        droplets = transform.GetChild(0).gameObject;
        droplets.GetComponent<Animator>().SetBool("isWet", true);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.realtimeSinceStartup - init_is_wet >= interval_is_wet)
        {
            m_Animator.SetBool("isWet", false);
            droplets.GetComponent<Animator>().SetBool("isWet", false);
            can_move = true;
            sourceDrop.Stop();
            sourceDrop.loop = false;
            sourceSplash.Stop();
            sourceSplash.loop = false;
        }
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
