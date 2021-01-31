using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnerSettings : MonoBehaviour
{
    private GameObject his_house;
    private Animator m_Animator = null;
    private bool isHappy = false;
    private float init_time_happy;
    private float interval_happy = 3.0f;
    private void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Time.realtimeSinceStartup - init_time_happy >= interval_happy)
        {
            m_Animator.SetBool("isHappy", false);
        }
    }

    public void SetHouse(GameObject house)
    {
        his_house = house;
    }


    public void UpdateAnimation()
    {
        m_Animator.SetBool("isHappy", true);
        isHappy = true; 
        init_time_happy = Time.realtimeSinceStartup;
    }
}
