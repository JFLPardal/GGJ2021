using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class OwnerSettings : MonoBehaviour
{
    private GameObject his_house;
    private Animator m_Animator = null;
    private bool isHappy = false;
    private float init_time_happy;
    private void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Time.realtimeSinceStartup - init_time_happy >= Constants.interval_owner_happy)
        {
            m_Animator.SetBool(Constants.animator_bool_owner_happy, false);
        }
    }

    public void SetHouse(GameObject house)
    {
        his_house = house;
    }


    public void UpdateAnimation()
    {
        m_Animator.SetBool(Constants.animator_bool_owner_happy, true);
        isHappy = true; 
        init_time_happy = Time.realtimeSinceStartup;
    }
}
